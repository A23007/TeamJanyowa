//Playerの動きを制御するスクリプト

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 移動速度
    public float bounceForce = 0f;  // 衝突時の跳ね返りの力

    private Rigidbody rb;  // Rigidbodyをキャッシュ

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Rigidbodyがない場合は追加
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // 入力をチェックして移動方向を決定
        float horizontal = 0f;  // 左右移動 (A、D)
        float vertical = 0f;    // 前後移動 (W、S)

        // Aキーで左に移動
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }
        // Dキーで右に移動
        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }
        // Wキーで前に移動
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }
        // Sキーで後ろに移動
        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        // 移動ベクトルを作成
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        // 移動処理
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


}
