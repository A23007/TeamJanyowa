//�G�̃X�e�[�^�X�y�у_���[�W�����@���c

using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 15;

    // �|�ꂽ�Ƃ��̃G�t�F�N�g�iInspector����ݒ�j
    public GameObject deathEffect;

    // �_���[�W���󂯂��Ƃ��̃G�t�F�N�g�iInspector����ݒ�j
    public GameObject damageEffect;  // �� �ǉ�

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"�G�� {damage} �_���[�W���󂯂� (�c��HP: {currentHP})");

        // �_���[�W�G�t�F�N�g�𐶐�
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            effect.AddComponent<AutoDestroyEffect>();  // �����폜�X�N���v�g���A�^�b�`
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("�G���|�ꂽ�I");

        // ���S�G�t�F�N�g�𐶐�
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.AddComponent<AutoDestroyEffect>();
        }

        Destroy(gameObject);
    }

    // �G�t�F�N�g���Đ���A�����ō폜���邽�߂̓����N���X
    public class AutoDestroyEffect : MonoBehaviour
    {
        void Start()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(gameObject, 5f); // �ی��Ƃ���5�b��ɍ폜
            }
        }
    }
}
