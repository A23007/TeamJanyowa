using UnityEngine;

//3方向に発射するスクリプトです。

public class Bullet3 : MonoBehaviour
{
    public GameObject bulletPrefab;      // 弾のプレハブ
    public float shootInterval = 1.5f;   // 発射間隔（秒）
    public float bulletSpeed = 10f;      // 弾の速度
    public float bulletLifetime = 5f;    // 弾の寿命（秒）
    public float spreadAngle = 15f;      // 左右にずらす角度（度数）

    private float timer = 0f;

    [System.Obsolete]
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            ShootSpread();
            timer = 0f;
        }
    }

    [System.Obsolete]
    void ShootSpread()
    {
        //anglesは角度
        // 発射方向：中央、左、右の3方向
        float[] angles = { -spreadAngle, 0f, spreadAngle };

        foreach (float angle in angles)
        {
            //Y軸に回転した方向に撃つ　参照はangle
            Quaternion bulletRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);

            // 弾の前方に速度を与える
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }

            // 一定時間で消える
            Destroy(bullet, bulletLifetime);
        }
    }
}
