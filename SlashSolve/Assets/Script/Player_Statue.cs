//プレイヤーステータス＆「ゲームオーバー処理　増田
//HP関係


using UnityEngine;
using TMPro; // ← これはそのまま
using UnityEngine.UI; // ← スライダーに必要
using UnityEngine.SceneManagement; // ← シーン遷移に必要

public class Player_Status : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;

    public TextMeshProUGUI hpText; // テキスト表示用
    public Slider hpSlider;        // ← スライダー表示用

    void Start()
    {
        currentHP = maxHP;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

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

        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }
    }

    void Die()
    {
        Debug.Log("プレイヤーが倒れた！");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene"); // ← 遷移先のシーン名に合わせて変更
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                TakeDamage(enemyStatus.attackPower);
                enemyStatus.TakeDamage(attackPower);
            }
        }

        if (collision.gameObject.CompareTag("RockObject") ||
            collision.gameObject.CompareTag("ScissorsObject") ||
            collision.gameObject.CompareTag("PaperObject"))
        {
            EnemyStatus enemyStatus = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                TakeDamage(enemyStatus.attackPower);
                enemyStatus.TakeDamage(attackPower);
            }
        }

        if (collision.gameObject.CompareTag("DamageObject"))
        {
            object_Damage damageObject = collision.gameObject.GetComponent<object_Damage>();
            if (damageObject != null)
            {
                TakeDamage(damageObject.attackPower);
                damageObject.TakeDamage(attackPower);
            }
        }

        if (collision.gameObject.CompareTag("BreakObject"))
        {
            BreakObject breakObject = collision.gameObject.GetComponent<BreakObject>();
            if (breakObject != null)
            {
                breakObject.TakeDamage(attackPower);
            }
        }
    }
}
