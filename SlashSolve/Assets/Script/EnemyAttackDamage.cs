


using UnityEngine;
using System.Collections.Generic;

public class EnemyAttackDamage : MonoBehaviour
{
    public int damage = 10;
    public float playerDamageMultiplier = 1f;
    public float attackCooldown = 1f; // �N�[���_�E�����ԁi�b�j

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private bool canAttack = true;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        if (part == null)
        {
            part = GetComponentInChildren<ParticleSystem>();
        }

        if (part == null)
        {
            Debug.LogError("ParticleSystem ��������܂���B");
        }

        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (!canAttack || part == null) return;

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<Player_Status>();
                if (player != null)
                {
                    player.TakeDamage(Mathf.RoundToInt(damage * playerDamageMultiplier));
                    StartCoroutine(AttackCooldown()); // �N�[���_�E���J�n
                    break; // ��x�q�b�g�����烋�[�v�𔲂���i���d�q�b�g�h�~�j
                }
            }
        }
    }

    private System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
