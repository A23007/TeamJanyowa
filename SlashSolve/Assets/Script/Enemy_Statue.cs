//Enemyステータス　増田

using UnityEngine;

//Enemyステータス　増田

using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 15;

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
        Destroy(gameObject);
    }
}
