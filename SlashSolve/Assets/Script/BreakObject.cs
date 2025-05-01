//�_���[�W���󂯂������I�u�W�F�N�g�@���c

using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public int maxHP = 30;
    private int currentHP;

    public GameObject destroyEffect; // �j�󎞃G�t�F�N�g
    public float effectLifetime = 2f; // �G�t�F�N�g��������܂ł̎��ԁi�b�j

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{gameObject.name} �� {damage} �̃_���[�W���󂯂��i�c��HP: {currentHP}�j");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (destroyEffect != null)
        {
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, effectLifetime); // �G�t�F�N�g����莞�Ԍ�ɔj��
        }

        Debug.Log($"{gameObject.name} �͔j�󂳂ꂽ�I");
        Destroy(gameObject);
    }
}
