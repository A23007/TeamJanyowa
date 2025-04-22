//ダメージ処理Enemy　増田
using UnityEngine;

public class Effect_Hit : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        Destroy(gameObject);
    }
}
