//カメラの動き　増田

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // 追いかけるターゲット
    public float distance = 5.0f; // ターゲットとの距離
    public float height = 3.0f; // カメラの高さ
    public float rotationSpeed = 90.0f; // カメラの回転速度（度/秒）
    public float smoothSpeed = 5.0f; // スムーズさの強さ（大きいほど速く追従）
    public float initialRotation = 0f; // 初期カメラ回転角度（Y軸）

    private float currentRotation;

    void Start()
    {
        currentRotation = initialRotation;
    }

    void LateUpdate()
    {
        HandleRotationInput();

        // 回転に基づくオフセットを計算
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // ターゲット位置からカメラの理想位置を計算
        Vector3 desiredPosition = target.position + Vector3.up * height + offset;

        // 現在の位置から理想位置へ滑らかに補間
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // カメラの回転をターゲット方向へ滑らかに補間
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }
    }
}

