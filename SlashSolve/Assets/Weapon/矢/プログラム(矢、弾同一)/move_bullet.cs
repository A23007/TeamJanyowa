using UnityEngine;



//弾を撃つ処理＆消す処理　インスペクターで調整できます。
//このスクリプトをプレイヤーにつけてほしいです。



public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;      // 弾のプレハブ
    public float shootInterval = 1.5f;   // 発射間隔（秒）
    public float bulletSpeed = 10f;      // 弾の速度
    public float bulletLifetime = 5f;    // 弾の寿命（秒）

    private float timer = 0f;

    [System.Obsolete]
    void Update()
    {
        //弾が撃ちだされる間隔
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            shoot();
            timer = 0f;
        }
    }
    
    [System.Obsolete]
    void shoot()
    {
        //弾を撃つ処理
        Quaternion rotatedRotation = transform.rotation * Quaternion.Euler(0, 0, 0);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotatedRotation);

        // Rigidbodyで前方向に力を加える
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * bulletSpeed; // 前方向（Z軸）に発射
        }

        // 一定時間後に自動で破壊
        Destroy(bullet, bulletLifetime);
    }
}
