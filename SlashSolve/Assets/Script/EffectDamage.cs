//エフェクトのダメージ判定 増田

using UnityEngine;
using System.Collections.Generic;

public class EffectDamage : MonoBehaviour
{
    public int damage = 10;

    [Header("Damage Multipliers")]
    public float breakObjectDamageMultiplier = 1f;
    public float enemyDamageMultiplier = 1f;
    public float rockObjectDamageMultiplier = 2f;
    public float scissorsObjectDamageMultiplier = 0.5f;
    public float paperObjectDamageMultiplier = 1f;

    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        // ParticleSystem を子オブジェクトも含めて探すように変更
        part = GetComponent<ParticleSystem>();
        if (part == null)
        {
            part = GetComponentInChildren<ParticleSystem>();
        }

        // エラーチェック：見つからなかった場合ログ出力
        if (part == null)
        {
            Debug.LogError("ParticleSystem が見つかりません。この GameObject または子オブジェクトに ParticleSystem を追加してください。");
        }

        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (part == null) return; // 念のため null チェック

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            switch (other.tag)
            {
                case "BreakObject":
                    var breakObj = other.GetComponent<BreakObject>();
                    if (breakObj != null)
                        breakObj.TakeDamage(Mathf.RoundToInt(damage * breakObjectDamageMultiplier));
                    break;

                case "Enemy":
                    var enemy = other.GetComponent<EnemyStatus>();
                    if (enemy != null)
                        enemy.TakeDamage(Mathf.RoundToInt(damage * enemyDamageMultiplier));
                    break;

                case "RockObject":
                    var rockObj = other.GetComponent<EnemyStatus>();
                    if (rockObj != null)
                        rockObj.TakeDamage(Mathf.RoundToInt(damage * rockObjectDamageMultiplier));
                    break;

                case "ScissorsObject":
                    var scissorsObj = other.GetComponent<EnemyStatus>();
                    if (scissorsObj != null)
                        scissorsObj.TakeDamage(Mathf.RoundToInt(damage * scissorsObjectDamageMultiplier));
                    break;

                case "PaperObject":
                    var paperObj = other.GetComponent<EnemyStatus>();
                    if (paperObj != null)
                        paperObj.TakeDamage(Mathf.RoundToInt(damage * paperObjectDamageMultiplier));
                    break;

                default:
                    break;
            }
        }
    }
}
