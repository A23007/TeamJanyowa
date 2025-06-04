


using UnityEngine;

public class Ice_Attack : MonoBehaviour
{
    [Header("生成するオブジェクト（例：Ice_Attack）")]
    public GameObject spawnPrefab;

    [Header("弾の速度")]
    public float speed = 10f;

    [Header("自動削除までの時間（秒）")]
    public float destroyAfterSeconds = 5f;

    void Update()
    {
        // 左クリックで生成
        if (Input.GetMouseButtonDown(0))
        {
            SpawnObjectTowardsMouse();
        }
    }

    void SpawnObjectTowardsMouse()
    {
        if (spawnPrefab == null)
        {
            Debug.LogWarning("spawnPrefab が設定されていません！");
            return;
        }

        // マウス位置を取得しワールド座標へ変換
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; // カメラからの距離
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);

        // 方向ベクトルを計算
        Vector3 direction = (targetPos - transform.position).normalized;

        // オブジェクトを生成（回転方向はオプション）
        GameObject spawned = Instantiate(spawnPrefab, transform.position, Quaternion.LookRotation(Vector3.forward, direction));

        // Rigidbody2Dが付いていれば速度を設定
        Rigidbody2D rb2D = spawned.GetComponent<Rigidbody2D>();
        if (rb2D != null)
        {
            rb2D.linearVelocity = direction * speed;
        }

        // Rigidbody（3D）の場合
        Rigidbody rb3D = spawned.GetComponent<Rigidbody>();
        if (rb3D != null)
        {
            rb3D.linearVelocity = direction * speed;
        }

        // 一定時間後に削除
        Destroy(spawned, destroyAfterSeconds);
    }
}

