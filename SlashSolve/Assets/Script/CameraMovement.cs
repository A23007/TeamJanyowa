//カメラの動き　増田

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // 追いかけるターゲット（例えば、プレイヤー）
    public float distance = 5.0f; // ターゲットとの距離
    public float height = 3.0f; // 高さ（カメラのY軸）
    public float rotationSpeed = 3.0f; // カメラの回転速度
    public float smoothSpeed = 0.125f; // スムーズに動かすための補間速度

    private float currentRotation = 0f; // カメラの現在の回転角度

    void Update()
    {
        // 矢印キーでカメラの回転を変更
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }

        // ターゲットの後ろに位置を設定
        Vector3 targetPosition = target.position + new Vector3(0, height, 0); // ターゲットの高さを調整

        // 回転を考慮したオフセットを計算
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance); // カメラの後ろに配置

        // スムーズにカメラの位置を更新
        Vector3 desiredPosition = targetPosition + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // スムーズにカメラがターゲットを向くようにする
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
    }
}
