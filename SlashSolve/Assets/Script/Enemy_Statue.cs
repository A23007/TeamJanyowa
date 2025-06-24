//敵のステータス及びダメージ処理　増田

using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;
    public int attackPower = 15;

    // 倒れたときのエフェクト（Inspectorから設定）
    public GameObject deathEffect;

    // ダメージを受けたときのエフェクト（Inspectorから設定）
    public GameObject damageEffect;  // ← 追加

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"敵が {damage} ダメージを受けた (残りHP: {currentHP})");

        // ダメージエフェクトを生成
        if (damageEffect != null)
        {
            GameObject effect = Instantiate(damageEffect, transform.position, Quaternion.identity);
            effect.AddComponent<AutoDestroyEffect>();  // 自動削除スクリプトをアタッチ
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("敵が倒れた！");

        // 死亡エフェクトを生成
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.AddComponent<AutoDestroyEffect>();
        }

        Destroy(gameObject);
    }

    // エフェクトを再生後、自動で削除するための内部クラス
    public class AutoDestroyEffect : MonoBehaviour
    {
        void Start()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
            }
            else
            {
                Destroy(gameObject, 5f); // 保険として5秒後に削除
            }
        }
    }
}
