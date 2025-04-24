//Enemyステータス　増田

using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 15;

    // 倒れたときのエフェクト（Inspectorから設定）
    public GameObject deathEffect;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"敵が {damage} ダメージを受けた (残りHP: {currentHP})");

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("敵が倒れた！");

        // エフェクトを生成（位置と回転は現在のオブジェクトに合わせる）
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            // エフェクトオブジェクトに自動削除スクリプトをアタッチ
            AutoDestroyEffect autoDestroy = effect.AddComponent<AutoDestroyEffect>();
        }

        Destroy(gameObject);
    }

    // エフェクトを再生後、自動で削除するための内部クラス
    public class AutoDestroyEffect : MonoBehaviour
    {
        void Start()
        {
            // ParticleSystemがある場合、その再生時間と最大ライフタイムに基づいて削除
            ParticleSystem ps = GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                // ParticleSystemがない場合は安全のため5秒後に削除（適宜調整）
                Destroy(gameObject, 5f);
            }
        }
    }
}
