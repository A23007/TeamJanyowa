// エフェクトをプレイヤーに合わせて動くようにする　増田


using UnityEngine;

public class Effect_Movement : MonoBehaviour
{
    [Tooltip("追従させたいターゲットオブジェクト")]
    public Transform target;

    [Tooltip("ターゲットからの相対位置（ワールド空間）")]
    public Vector3 offset = Vector3.zero;

    [Tooltip("ターゲットの回転に追従するかどうか")]
    public bool useRotation = false;

    [Header("周回設定")]
    [Tooltip("Playerの周りを回るかどうか")]
    public bool orbitAroundTarget = false;

    [Tooltip("回転半径（XZ平面）")]
    public float orbitRadius = 2f;

    [Tooltip("回転速度（度/秒）")]
    public float orbitSpeed = 90f;

    private float currentAngle = 0f;

    void Start()
    {
        // Playerタグを持つオブジェクトを探してtargetに設定
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Playerタグを持つオブジェクトが見つかりませんでした。");
        }

        // 初期角度の計算（オフセットを元に）
        if (orbitAroundTarget)
        {
            Vector3 offsetXZ = new Vector3(offset.x, 0, offset.z);
            if (offsetXZ.magnitude > 0f)
            {
                currentAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            if (orbitAroundTarget)
            {
                // 時間経過で角度を更新
                currentAngle += orbitSpeed * Time.deltaTime;
                if (currentAngle > 360f) currentAngle -= 360f;

                // 新しい位置を計算（XZ平面で円を描く）
                float rad = currentAngle * Mathf.Deg2Rad;
                Vector3 orbitOffset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * orbitRadius;

                // ターゲット位置にオフセットを加える（Y軸の高さは offset.y を使用）
                transform.position = target.position + new Vector3(orbitOffset.x, offset.y, orbitOffset.z);
            }
            else
            {
                // ターゲットの位置に固定オフセットを加算
                transform.position = target.position + offset;
            }

            if (useRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
