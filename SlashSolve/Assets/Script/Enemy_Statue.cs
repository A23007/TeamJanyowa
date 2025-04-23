//Enemy�X�e�[�^�X�@���c

using UnityEngine;

// Enemy�X�e�[�^�X�@���c

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 15;

    // �|�ꂽ�Ƃ��̃G�t�F�N�g�iInspector����ݒ�j
    public GameObject deathEffect;

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

        // �G�t�F�N�g�𐶐��i�ʒu�Ɖ�]�͌��݂̃I�u�W�F�N�g�ɍ��킹��j
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
