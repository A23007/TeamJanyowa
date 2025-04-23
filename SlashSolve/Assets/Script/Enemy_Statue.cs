//Enemyステータス　増田

using UnityEngine;

// Enemyステータス　増田

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 15;

    // 倒れたときのエフェクト（Inspectorから設定）
    public GameObject deathEffect;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"敵が {damage} ダメージを受けた (残りHP: {currentHP})");

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("敵が倒れた！");

        // エフェクトを生成（位置と回転は現在のオブジェクトに合わせる）
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
