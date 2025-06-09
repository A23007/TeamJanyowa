using UnityEngine;

public class IceBreakSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject deathEffect; // 死亡エフェクトのPrefab
    public float effectLifetime = 2f; // エフェクトの寿命（秒）

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fireAttack"))
        {
            TakeDamage(10); // 任意のダメージ値
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("ダメージを受けた！ 残りHP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("死亡しました。");

        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(effect, effectLifetime); // 一定時間後にエフェクトを自動破棄
        }

        Destroy(gameObject); // このオブジェクトを削除
    }
}
