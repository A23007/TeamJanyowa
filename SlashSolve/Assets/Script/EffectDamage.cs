//エフェクトのダメージ判定 増田

using UnityEngine;

public class ParticleDamageDealer : MonoBehaviour
{
    public int damage = 10;
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
            Effect_Hit receiver = other.GetComponent<Effect_Hit>();
            if (receiver != null)
            {
                receiver.TakeDamage(damage);
            }
        }
    }
}
