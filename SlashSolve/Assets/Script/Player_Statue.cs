//�v���C���[�X�e�[�^�X�@���c

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
        Debug.Log($"�v���C���[�� {damage} �_���[�W���󂯂� (�c��HP: {currentHP})");

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�v���C���[���|�ꂽ�I");
        // �Q�[���I�[�o�[�����Ȃ�
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyStatus enemy = collision.gameObject.GetComponent<EnemyStatus>();
            if (enemy != null)
            {
                // ���݂��Ƀ_���[�W��^����
                TakeDamage(enemy.attackPower);
                enemy.TakeDamage(attackPower);
            }
        }
    }
}
