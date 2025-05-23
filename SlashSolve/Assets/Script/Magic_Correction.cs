//魔法使い用補正　増田

using UnityEngine;

public class CenterObjectToCollider : MonoBehaviour
{
    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning("Colliderが見つかりません");
            return;
        }

        // Colliderの中心（ワールド座標）
        Vector3 colliderCenterWorld = col.bounds.center;

        // 自分の現在の位置との差分
        Vector3 offset = colliderCenterWorld - transform.position;

        // 全ての子をオフセットで逆に動かす（見た目の補正）
        foreach (Transform child in transform)
        {
            child.position -= offset;
        }

        // 自身の位置をCollider中心に
        transform.position = colliderCenterWorld;
    }
}
