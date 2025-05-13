//Playerの動きを制御するスクリプト

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // プレイヤーの移動速度
    public float stopDistance = 0.1f;   // 十分近づいたら停止するためのしきい値

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;  // 必要に応じて重力無効化
        rb.constraints = RigidbodyConstraints.FreezeRotation;  // 回転を固定
    }

    void Update()
    {
        // マウスカーソルからRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 地面などのColliderにRayが当たった場合
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = (targetPosition - transform.position);
            direction.y = 0f; // 高さ方向の移動を無視

            if (direction.magnitude > stopDistance)
            {
                Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
                transform.position += move;

                // プレイヤーをマウス方向に向ける（任意）
                if (move != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(move);
                }
            }
        }
    }
}
