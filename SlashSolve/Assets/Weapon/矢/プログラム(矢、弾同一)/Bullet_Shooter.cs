//飛び攻撃処理（3move_bullet,move_bullet）参照　増田

using UnityEngine;
using System.Collections;

public class BulletShooter : MonoBehaviour
{
    public enum FireMode
    {
        Single,
        Triple
    }

    public FireMode fireMode = FireMode.Single;
    public GameObject bulletPrefab;
    public float shootInterval = 1.5f;
    public float moveSpeed = 20f;
    public float bulletLifetime = 5f;
    public float spreadAngle = 15f;

    public float shootHeight = 1.0f;  // 発射位置の高さ(Y座標)

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
            Vector3 shootDirection = GetMouseWorldDirection();

            switch (fireMode)
            {
                case FireMode.Single:
                    ShootBullet(shootDirection);
                    break;
                case FireMode.Triple:
                    ShootBullet(GetDirection(shootDirection, -spreadAngle));
                    ShootBullet(shootDirection);
                    ShootBullet(GetDirection(shootDirection, spreadAngle));
                    break;
            }

            timer = 0f;
        }
    }

    // マウスカーソルの方向をワールド空間で取得（Yは固定）
    Vector3 GetMouseWorldDirection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Vector3 direction = hitPoint - transform.position;

            // 水平方向のみ（Y成分を0に）
            direction.y = 0;
            return direction.normalized;
        }

        return transform.forward; // デフォルト方向
    }

    // 基準方向に対してY軸周りにangle度回転させた方向を返す
    Vector3 GetDirection(Vector3 baseDirection, float angle)
    {
        return Quaternion.Euler(0f, angle, 0f) * baseDirection;
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

        bullet.AddComponent<BulletBehavior>().Initialize(direction, moveSpeed);
        Destroy(bullet, bulletLifetime);
    }
}

public class BulletBehavior : MonoBehaviour
{
    private Vector3 moveDirection;
    private float speed = 20f;
    private bool isMoving = true;

    public void Initialize(Vector3 direction, float moveSpeed)
    {
        moveDirection = direction.normalized;
        speed = moveSpeed;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
        }
    }
}

