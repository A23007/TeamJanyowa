//ダメージ処理　増田

using UnityEngine;


public class WeaponDamage : MonoBehaviour
{
    public int damage = 10;

    [Header("Damage Multipliers")]
    public float breakObjectDamageMultiplier = 1f;
    public float enemyDamageMultiplier = 1f;
    public float rockObjectDamageMultiplier = 2f;
    public float scissorsObjectDamageMultiplier = 0.5f;
    public float paperObjectDamageMultiplier = 1f;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BreakObject":
                BreakObject breakObj = other.GetComponent<BreakObject>();
                if (breakObj != null)
                {
                    breakObj.TakeDamage(Mathf.RoundToInt(damage * breakObjectDamageMultiplier));
                }
                break;

            case "Enemy":
                EnemyStatus enemy = other.GetComponent<EnemyStatus>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Mathf.RoundToInt(damage * enemyDamageMultiplier));
                }
                break;

            case "RockObject":
                EnemyStatus rockObj = other.GetComponent<EnemyStatus>();
                if (rockObj != null)
                {
                    rockObj.TakeDamage(Mathf.RoundToInt(damage * rockObjectDamageMultiplier));
                }
                break;

            case "ScissorsObject":
                EnemyStatus scissorsObj = other.GetComponent<EnemyStatus>();
                if (scissorsObj != null)
                {
                    scissorsObj.TakeDamage(Mathf.RoundToInt(damage * scissorsObjectDamageMultiplier));
                }
                break;

            case "PaperObject":
                EnemyStatus paperObj = other.GetComponent<EnemyStatus>();
                if (paperObj != null)
                {
                    paperObj.TakeDamage(Mathf.RoundToInt(damage * paperObjectDamageMultiplier));
                }
                break;

            default:
                // 未対応タグは無視
                break;
        }
    }
}
