//敵の基本的な動作 増田

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RandomMovement : MonoBehaviour
{
    public Transform moveArea;
    public float moveSpeed = 3f;
    public float stopTime = 3f;
    public float detectRange = 5f;
    public float lostTargetWaitTime = 5f;

    public enum FaceDirectionType
    {
        Forward,
        Backward,
        Left,
        Right
    }

    public FaceDirectionType faceDirection = FaceDirectionType.Forward;

    private Transform targetCharacter;
    private Vector3 destination;
    private Rigidbody rb;
    private Collider col;

    private enum State
    {
        Wandering,
        Chasing,
        WaitingAfterChase
    }

    private State currentState = State.Wandering;
    private Coroutine currentRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        // 地面に着地させる
        Vector3 startPosition = transform.position;
        startPosition.y = GetGroundYAtPosition(startPosition);
        transform.position = startPosition;

        currentRoutine = StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Wandering:
                DetectCharacter();
                break;

            case State.Chasing:
                if (targetCharacter != null)
                {
                    float distance = Vector3.Distance(transform.position, targetCharacter.position);
                    if (distance > detectRange)
                    {
                        targetCharacter = null;
                        currentState = State.WaitingAfterChase;
                        if (currentRoutine != null) StopCoroutine(currentRoutine);
                        currentRoutine = StartCoroutine(WaitThenResume());
                    }
                    else
                    {
                        MoveTowards(targetCharacter.position);
                    }
                }
                break;

            case State.WaitingAfterChase:
                break;
        }
    }

    void DetectCharacter()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (var character in characters)
        {
            float distance = Vector3.Distance(transform.position, character.transform.position);
            if (distance <= detectRange)
            {
                targetCharacter = character.transform;
                currentState = State.Chasing;
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                break;
            }
        }
    }

    IEnumerator WanderRoutine()
    {
        currentState = State.Wandering;

        while (true)
        {
            destination = GetRandomPointOnSurface();
            yield return StartCoroutine(MoveToPosition(destination));
            yield return new WaitForSeconds(stopTime);
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            if (currentState != State.Wandering)
                yield break;

            MoveTowards(targetPos);
            yield return null;
        }
    }

    IEnumerator WaitThenResume()
    {
        yield return new WaitForSeconds(lostTargetWaitTime);

        currentState = State.Wandering;
        currentRoutine = StartCoroutine(WanderRoutine());
    }

    void MoveTowards(Vector3 targetPos)
    {
        Vector3 currentPos = transform.position;

        // Y座標を維持
        targetPos.y = currentPos.y;

        Vector3 direction = (targetPos - currentPos).normalized;

        // 向きをターゲット方向に向ける（FaceDirectionType に応じて補正）
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            switch (faceDirection)
            {
                case FaceDirectionType.Backward:
                    targetRotation *= Quaternion.Euler(0f, 180f, 0f);
                    break;
                case FaceDirectionType.Left:
                    targetRotation *= Quaternion.Euler(0f, -90f, 0f);
                    break;
                case FaceDirectionType.Right:
                    targetRotation *= Quaternion.Euler(0f, 90f, 0f);
                    break;
                    // Forward はそのまま
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        Vector3 newPosition = currentPos + direction * moveSpeed * Time.deltaTime;
        newPosition.y = currentPos.y;

        rb.MovePosition(newPosition);
    }

    Vector3 GetRandomPointOnSurface()
    {
        Vector3 areaSize = moveArea.localScale;
        Vector3 areaCenter = moveArea.position;

        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

        Vector3 point = new Vector3(areaCenter.x + x, 100f, areaCenter.z + z); // 高い位置で Ray を飛ばす
        point.y = GetGroundYAtPosition(point);
        return point;
    }

    float GetGroundYAtPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, 100f))
        {
            return hit.point.y + col.bounds.extents.y;
        }
        return position.y;
    }
}
