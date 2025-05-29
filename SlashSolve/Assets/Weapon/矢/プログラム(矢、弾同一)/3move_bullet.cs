using UnityEngine;

//3�����ɔ��˂���X�N���v�g�ł��B

public class Bullet3 : MonoBehaviour
{
    public GameObject bulletPrefab;      // �e�̃v���n�u
    public float shootInterval = 1.5f;   // ���ˊԊu�i�b�j
    public float bulletSpeed = 10f;      // �e�̑��x
    public float bulletLifetime = 5f;    // �e�̎����i�b�j
    public float spreadAngle = 15f;      // ���E�ɂ��炷�p�x�i�x���j

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
        //angles�͊p�x
        // ���˕����F�����A���A�E��3����
        float[] angles = { -spreadAngle, 0f, spreadAngle };

        foreach (float angle in angles)
        {
            //Y���ɉ�]���������Ɍ��@�Q�Ƃ�angle
            Quaternion bulletRotation = transform.rotation * Quaternion.Euler(0, angle, 0);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);

            // �e�̑O���ɑ��x��^����
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }

            // ��莞�Ԃŏ�����
            Destroy(bullet, bulletLifetime);
        }
    }
}
