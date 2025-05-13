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
    }

    void Update()
    {
        if (target != null)
        {
            // ターゲットの位置に、ワールド空間での固定オフセットを加算
            transform.position = target.position + offset;

            if (useRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
