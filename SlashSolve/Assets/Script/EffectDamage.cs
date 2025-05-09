//エフェクトのダメージ判定 増田

using UnityEngine;

public class EffectDamage : MonoBehaviour
{
    public int damage = 10;

    // ダメージ倍率を Unity Inspector 上で設定できるように
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
                        // BreakObjectのダメージ倍率を適用
                        breakObj.TakeDamage(Mathf.RoundToInt(damage * breakObjectDamageMultiplier));
                    }
                    break;

                case "Enemy":
                    EnemyStatus enemy = other.GetComponent<EnemyStatus>();
                    if (enemy != null)
                    {
                        // Enemyのダメージ倍率を適用
                        enemy.TakeDamage(Mathf.RoundToInt(damage * enemyDamageMultiplier));
                    }
                    break;

                case "RockObject":
                    EnemyStatus rockObj = other.GetComponent<EnemyStatus>();
                    if (rockObj != null)
                    {
                        // RockObjectのダメージ倍率を適用
                        rockObj.TakeDamage(Mathf.RoundToInt(damage * rockObjectDamageMultiplier));
                    }
                    break;

                case "ScissorsObject":
                    EnemyStatus scissorsObj = other.GetComponent<EnemyStatus>();
                    if (scissorsObj != null)
                    {
                        // ScissorsObjectのダメージ倍率を適用
                        scissorsObj.TakeDamage(Mathf.RoundToInt(damage * scissorsObjectDamageMultiplier));
                    }
                    break;

                case "PaperObject":
                    EnemyStatus paperObj = other.GetComponent<EnemyStatus>();
                    if (paperObj != null)
                    {
                        // PaperObjectのダメージ倍率を適用
                        paperObj.TakeDamage(Mathf.RoundToInt(damage * paperObjectDamageMultiplier));
                    }
                    break;

                default:
                    // 対応していないタグは無視
                    break;
            }
        }
    }
}
