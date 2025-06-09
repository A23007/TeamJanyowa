using UnityEngine;

public class IceBreakSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject deathEffect; // ���S�G�t�F�N�g��Prefab
    public float effectLifetime = 2f; // �G�t�F�N�g�̎����i�b�j

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fireAttack"))
        {
            TakeDamage(10); // �C�ӂ̃_���[�W�l
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("�_���[�W���󂯂��I �c��HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���S���܂����B");

        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(effect, effectLifetime); // ��莞�Ԍ�ɃG�t�F�N�g�������j��
        }

        Destroy(gameObject); // ���̃I�u�W�F�N�g���폜
    }
}
