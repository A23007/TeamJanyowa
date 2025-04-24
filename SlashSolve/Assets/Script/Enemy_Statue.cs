//Enemy�X�e�[�^�X�@���c

using UnityEngine;

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
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            // �G�t�F�N�g�I�u�W�F�N�g�Ɏ����폜�X�N���v�g���A�^�b�`
            AutoDestroyEffect autoDestroy = effect.AddComponent<AutoDestroyEffect>();
        }

        Destroy(gameObject);
    }

    // �G�t�F�N�g���Đ���A�����ō폜���邽�߂̓����N���X
    public class AutoDestroyEffect : MonoBehaviour
    {
        void Start()
        {
            // ParticleSystem������ꍇ�A���̍Đ����Ԃƍő僉�C�t�^�C���Ɋ�Â��č폜
            ParticleSystem ps = GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                // ParticleSystem���Ȃ��ꍇ�͈��S�̂���5�b��ɍ폜�i�K�X�����j
                Destroy(gameObject, 5f);
            }
        }
    }
}
