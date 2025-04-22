//ダメージ処理Enemy　増田
using UnityEngine;

public class Effect_Hit : MonoBehaviour
{
    private EnemyStatus enemyStatus;

    void Start()
    {
        // 同じオブジェクトにアタッチされている EnemyStatus を取得
        enemyStatus = GetComponent<EnemyStatus>();

        if (enemyStatus == null)
        {
            Debug.LogWarning("EnemyStatus が見つかりませんでした。");
        }
    }

    public void TakeDamage(int amount)
    {
        if (enemyStatus != null)
        {
            enemyStatus.TakeDamage(amount);  // EnemyStatus にダメージ処理を任せる
        }
        else
        {
            Debug.LogWarning("EnemyStatus が未設定のためダメージを処理できません。");
        }
    }
}
