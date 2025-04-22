//エフェクトをプレイヤーに合わせて動くようにする　増田

using UnityEngine;


public class Effect_Movement : MonoBehaviour
{
    [Tooltip("追従させたいターゲットオブジェクト")]
    public Transform target;

    [Tooltip("ターゲットからの相対位置（ローカル座標）")]
    public Vector3 offset = Vector3.zero;

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
