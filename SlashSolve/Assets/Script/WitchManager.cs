
using UnityEngine;

public class WitchManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOrigin;
    public float detectionRadius = 10f;
    public float shootInterval = 1.5f;
    public float moveSpeed = 20f;
    public float bulletLifetime = 5f;

    private float timer = 0f;

    void Start()
    {
        if (shootOrigin == null)
        {
            shootOrigin = transform;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            GameObject target = FindNearestPlayer();
            if (target != null)
            {
                Vector3 direction = (target.transform.position - shootOrigin.position).normalized;
                direction.y = 0f;
                ShootBullet(direction);
            }

            timer = 0f;
        }
    }

    GameObject FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= detectionRadius && distance < minDistance)
            {
                nearest = player;
                minDistance = distance;
            }
        }

        return nearest;
    }

    void ShootBullet(Vector3 direction)
    {
        float yHeight = shootOrigin.position.y;
        Renderer renderer = shootOrigin.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            yHeight = renderer.bounds.center.y;
        }

        Vector3 spawnPos = new Vector3(
            shootOrigin.position.x + direction.x * 0.5f,
            yHeight,
            shootOrigin.position.z + direction.z * 0.5f
        );

        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            bullet.AddComponent<BulletMover>().Initialize(direction, moveSpeed);
        }

        Destroy(bullet, bulletLifetime);
    }
}
