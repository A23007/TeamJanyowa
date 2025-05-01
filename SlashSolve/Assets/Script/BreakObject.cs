//ダメージを受けたら壊れるオブジェクト　増田

using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public int maxHP = 30;
    private int currentHP;

    public GameObject destroyEffect; // 破壊時エフェクト
    public float effectLifetime = 2f; // エフェクトが消えるまでの時間（秒）

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{gameObject.name} は {damage} のダメージを受けた（残りHP: {currentHP}）");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (destroyEffect != null)
        {
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectLifetime); // エフェクトを一定時間後に破棄
        }

        Debug.Log($"{gameObject.name} は破壊された！");
        Destroy(gameObject);
    }
}
