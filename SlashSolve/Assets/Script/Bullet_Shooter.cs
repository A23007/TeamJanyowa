//飛び攻撃処理（3move_bullet,move_bullet）参照　増田

using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOrigin; // 発射位置を指定（自動設定あり）
    public float shootInterval = 1.5f;
    public float moveSpeed = 20f;
    public float bulletLifetime = 5f;
    public float spreadAngle = 30f;
    public int bulletCount = 5;

    private float timer = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // shootOriginが指定されていない場合、Playerタグを探す
        if (shootOrigin == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                shootOrigin = player.transform;
            }
            else
            {
                Debug.LogWarning("shootOriginが指定されておらず、Playerタグのオブジェクトも見つかりません。自身のTransformを使用します。");
                shootOrigin = transform;
            }
        }
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

    // マウスの位置からワールド空間上の方向を取得
    Vector3 GetMouseWorldDirection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = hitPoint - shootOrigin.position;
            direction.y = 0;
            return direction.normalized;
        }

        return shootOrigin.forward;
    }

    // 指定された方向に複数の弾を発射（スプレッドあり）
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

    // 弾を1発発射
    void ShootBullet(Vector3 direction)
    {
        // 発射位置のY座標をオブジェクトの中央にする
        float yHeight = shootOrigin.position.y;
        Renderer renderer = shootOrigin.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            yHeight = renderer.bounds.center.y;
        }

        Vector3 spawnPos = new Vector3(
            shootOrigin.position.x + direction.normalized.x * 0.5f,
            yHeight,
            shootOrigin.position.z + direction.normalized.z * 0.5f
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

// 弾の移動用クラス
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
            Destroy(gameObject);
        }
    }
}
