//�v���C���[�X�e�[�^�X���u�Q�[���I�[�o�[�����@���c
//HP�֌W


using UnityEngine;
using TMPro; // �� ����͂��̂܂�
using UnityEngine.UI; // �� �X���C�_�[�ɕK�v
using UnityEngine.SceneManagement; // �� �V�[���J�ڂɕK�v

public class Player_Status : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;
    public int attackPower = 20;

    public TextMeshProUGUI hpText; // �e�L�X�g�\���p
    public Slider hpSlider;        // �� �X���C�_�[�\���p

    void Start()
    {
        currentHP = maxHP;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

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

        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }
    }

    void Die()
    {
        Debug.Log("�v���C���[���|�ꂽ�I");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene"); // �� �J�ڐ�̃V�[�����ɍ��킹�ĕύX
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
