//Enemy�X�e�[�^�X�@���c

using UnityEngine;

//Enemy�X�e�[�^�X�@���c

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
        Debug.Log($"�G�� {damage} �_���[�W���󂯂� (�c��HP: {currentHP})");

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�G���|�ꂽ�I");
        Destroy(gameObject);
    }
}
