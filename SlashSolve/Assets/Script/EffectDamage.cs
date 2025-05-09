//�G�t�F�N�g�̃_���[�W���� ���c

using UnityEngine;

public class EffectDamage : MonoBehaviour
{
    public int damage = 10;

    // �_���[�W�{���� Unity Inspector ��Őݒ�ł���悤��
    [Header("Damage Multipliers")]
    public float breakObjectDamageMultiplier = 1f;
    public float enemyDamageMultiplier = 1f;
    public float rockObjectDamageMultiplier = 2f;
    public float scissorsObjectDamageMultiplier = 0.5f;
    public float paperObjectDamageMultiplier = 1f;

    private ParticleSystem part;
    private ParticleCollisionEvent[] collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[16];
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            switch (other.tag)
            {
                case "BreakObject":
                    BreakObject breakObj = other.GetComponent<BreakObject>();
                    if (breakObj != null)
                    {
                        // BreakObject�̃_���[�W�{����K�p
                        breakObj.TakeDamage(Mathf.RoundToInt(damage * breakObjectDamageMultiplier));
                    }
                    break;

                case "Enemy":
                    EnemyStatus enemy = other.GetComponent<EnemyStatus>();
                    if (enemy != null)
                    {
                        // Enemy�̃_���[�W�{����K�p
                        enemy.TakeDamage(Mathf.RoundToInt(damage * enemyDamageMultiplier));
                    }
                    break;

                case "RockObject":
                    EnemyStatus rockObj = other.GetComponent<EnemyStatus>();
                    if (rockObj != null)
                    {
                        // RockObject�̃_���[�W�{����K�p
                        rockObj.TakeDamage(Mathf.RoundToInt(damage * rockObjectDamageMultiplier));
                    }
                    break;

                case "ScissorsObject":
                    EnemyStatus scissorsObj = other.GetComponent<EnemyStatus>();
                    if (scissorsObj != null)
                    {
                        // ScissorsObject�̃_���[�W�{����K�p
                        scissorsObj.TakeDamage(Mathf.RoundToInt(damage * scissorsObjectDamageMultiplier));
                    }
                    break;

                case "PaperObject":
                    EnemyStatus paperObj = other.GetComponent<EnemyStatus>();
                    if (paperObj != null)
                    {
                        // PaperObject�̃_���[�W�{����K�p
                        paperObj.TakeDamage(Mathf.RoundToInt(damage * paperObjectDamageMultiplier));
                    }
                    break;

                default:
                    // �Ή����Ă��Ȃ��^�O�͖���
                    break;
            }
        }
    }
}
