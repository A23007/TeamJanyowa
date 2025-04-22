//プレイヤーステータス　増田

using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"プレイヤーが {damage} ダメージを受けた (残りHP: {currentHP})");

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("プレイヤーが倒れた！");
        // ゲームオーバー処理など
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemy != null)
            {
                // お互いにダメージを与える
                TakeDamage(enemy.attackPower);
                enemy.TakeDamage(attackPower);
            }
        }
    }
}
