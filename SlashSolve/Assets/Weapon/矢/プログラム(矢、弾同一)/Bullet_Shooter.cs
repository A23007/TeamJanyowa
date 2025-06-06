//飛び攻撃処理（3move_bullet,move_bullet）参照　増田

using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval = 1.5f;
    public float moveSpeed = 20f;
    public float bulletLifetime = 5f;
    public float spreadAngle = 30f; // 総スプレッド角度（例：30°なら -15°〜+15°に分散）
    public int bulletCount = 5;     // 発射する弾の数（奇数なら中央にも出る）

    public float shootHeight = 1.0f;

    private float timer = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Vector3 baseDirection = GetMouseWorldDirection();
            FireSpreadBullets(baseDirection);
            timer = 0f;
        }
    }

    Vector3 GetMouseWorldDirection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        return transform.forward;
    }

    void FireSpreadBullets(Vector3 baseDirection)
    {
        if (bulletCount <= 1)
        {
            ShootBullet(baseDirection);
            return;
        }

        float angleStep = (bulletCount > 1) ? spreadAngle / (bulletCount - 1) : 0f;
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 direction = Quaternion.Euler(0f, angle, 0f) * baseDirection;
            ShootBullet(direction);
        }
    }

    void ShootBullet(Vector3 direction)
    {
        Vector3 spawnPos = new Vector3(
            transform.position.x + direction.normalized.x * 0.5f,
            shootHeight,
            transform.position.z + direction.normalized.z * 0.5f
        );

        Quaternion rotation = Quaternion.LookRotation(direction);
        GameObject bullet = Instantiate(bulletPrefab, spawnPos, rotation);

        // Rigidbodyがある場合はそれを使って速度を与える
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            // Rigidbody が無ければ自動で動かすコンポーネントを追加
            bullet.AddComponent<BulletMover>().Initialize(direction, moveSpeed);
        }

        Destroy(bullet, bulletLifetime);
    }
}

// 補助的な移動処理（この中で完結）
public class BulletMover : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed;

    public void Initialize(Vector3 direction, float moveSpeed)
    {
        moveDirection = direction.normalized;
        speed = moveSpeed;
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // 衝突時に消える
        }
    }
}
