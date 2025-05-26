//Playerの動きを制御するスクリプト 増田

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;              // プレイヤーの移動速度
    public float stopDistance = 0.1f;         // 十分近づいたら停止するためのしきい値

    public enum FacingDirection
    {
        Forward,  // Z+
        Right,    // X+
        Left,     // X-
        Back,     // Z-
        Up,       // Y+
        Down      // Y-
    }

    public FacingDirection facing = FacingDirection.Forward;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        if (hits.Length > 0)
        {
            // 一番奥のヒットを取得（距離が最も遠いもの）
            RaycastHit farthestHit = hits[0];
            foreach (var hit in hits)
            {
                if (hit.distance > farthestHit.distance)
                {
                    farthestHit = hit;
                }
            }

            Vector3 targetPosition = farthestHit.point;
            Vector3 currentPosition = transform.position;

            // Y座標を維持（地面と同じ高さを保つ）
            targetPosition.y = currentPosition.y;

            Vector3 direction = targetPosition - currentPosition;

            if (direction.magnitude > stopDistance)
            {
                Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
                rb.MovePosition(currentPosition + move);

                if (move != Vector3.zero)
                {
                    ApplyFacingDirection(move.normalized);
                }
            }
        }
    }

    void ApplyFacingDirection(Vector3 moveDir)
    {
        switch (facing)
        {
            case FacingDirection.Forward:
                transform.rotation = Quaternion.LookRotation(moveDir);
                break;
            case FacingDirection.Back:
                transform.rotation = Quaternion.LookRotation(-moveDir);
                break;
            case FacingDirection.Right:
                {
                    Vector3 forward = Vector3.Cross(Vector3.up, moveDir);
                    transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
                    break;
                }
            case FacingDirection.Left:
                {
                    Vector3 forward = Vector3.Cross(moveDir, Vector3.up);
                    transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
                    break;
                }
            case FacingDirection.Up:
                {
                    Vector3 forward = Vector3.Cross(moveDir, Vector3.right);
                    transform.rotation = Quaternion.LookRotation(forward, Vector3.right);
                    break;
                }
            case FacingDirection.Down:
                {
                    Vector3 forward = Vector3.Cross(Vector3.right, moveDir);
                    transform.rotation = Quaternion.LookRotation(forward, Vector3.right);
                    break;
                }
        }
    }
}
