//プレイヤーステータス　増田

using UnityEngine;
using TMPro; // ← これを忘れずに

public class Player_Status : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;

    public TextMeshProUGUI hpText; // ← UIとの接続用

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText(); // 初期表示
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"プレイヤーが {damage} ダメージを受けた (残りHP: {currentHP})");

        UpdateHPText();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        UpdateHPText();
    }

    void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = $"HP: {currentHP}";
        }
    }

    void Die()
    {
        Debug.Log("プレイヤーが倒れた！");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemy != null)
            {
                TakeDamage(enemy.attackPower);
                enemy.TakeDamage(attackPower);
            }
        }
    }
}
