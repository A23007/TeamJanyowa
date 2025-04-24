// エフェクトをプレイヤーに合わせて動くようにする　増田


using UnityEngine;

public class Effect_Movement : MonoBehaviour
{
    [Tooltip("追従させたいターゲットオブジェクト")]
    public Transform target;

    [Tooltip("ターゲットからの相対位置（ローカル座標）")]
    public Vector3 offset = Vector3.zero;

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
            // ターゲットのローカル方向にオフセットを加える
            transform.position = target.position + target.rotation * offset;
            transform.rotation = target.rotation;
        }
    }
}
