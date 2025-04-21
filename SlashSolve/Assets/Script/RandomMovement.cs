//敵の基本的な動作

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))] // Colliderの取得を追加
public class RandomMovement : MonoBehaviour
{
    public Transform moveArea;
    public float moveSpeed = 3f;
    public float stopTime = 3f;
    public float detectRange = 5f;
    public float lostTargetWaitTime = 5f;

    private Transform targetCharacter;
    private Vector3 destination;
    private Rigidbody rb;
    private Collider col; // Colliderを追加

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
        col = GetComponent<Collider>(); // Colliderを取得
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
        Vector3 newPosition = currentPos + direction * moveSpeed * Time.deltaTime;

        // Y座標を固定
        newPosition.y = currentPos.y;

        rb.MovePosition(newPosition);
    }

    Vector3 GetRandomPointOnSurface()
    {
        Vector3 areaSize = moveArea.localScale;
        Vector3 areaCenter = moveArea.position;

        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);

        Vector3 point = new Vector3(areaCenter.x + x, 100f, areaCenter.z + z); // かなり高い位置でRayを飛ばす

        // 地面の表面に合わせる
        point.y = GetGroundYAtPosition(point);
        return point;
    }

    float GetGroundYAtPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, 100f))
        {
            // Colliderの半径を考慮して地面から距離を取る
            return hit.point.y + col.bounds.extents.y;
        }
        return position.y; // 地面がなければそのまま
    }
}

