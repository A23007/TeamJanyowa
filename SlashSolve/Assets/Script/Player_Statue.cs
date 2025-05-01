//�v���C���[�X�e�[�^�X�@���c

using UnityEngine;
using TMPro; // �� �����Y�ꂸ��

public class Player_Status : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;

    public TextMeshProUGUI hpText; // �� UI�Ƃ̐ڑ��p

    void Start()
    {
        currentHP = maxHP;
        UpdateHPText(); // �����\��
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"�v���C���[�� {damage} �_���[�W���󂯂� (�c��HP: {currentHP})");

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
        Debug.Log("�v���C���[���|�ꂽ�I");
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
            BreakObject breakObject = collision.gameObject.GetComponent<BreakObject>();  // ������BreakObject���擾
            if (breakObject != null)
            {
                breakObject.TakeDamage(attackPower); // �v���C���[�̍U���͂Ń_���[�W��^����
            }
        }
    }
}