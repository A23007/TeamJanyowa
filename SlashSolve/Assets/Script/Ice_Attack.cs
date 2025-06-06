


using UnityEngine;
using System.Linq;

public class Ice_Attack : MonoBehaviour
{
    public GameObject objectToSpawn;     // 生成するプレハブ
    public float spawnInterval = 1.0f;   // 生成間隔（秒）
    public float spawnDistance = 2.0f;   // 生成距離
    public float lifeTime = 5.0f;        // 存続時間
    public float fixedYPosition = 0.0f;  // 生成時のY座標固定

    private float timer = 0f;
    private Vector3 lastPosition;
    private Vector3 lastMoveDirection = Vector3.right;  // 初期方向は右向き

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 現在の移動ベクトルを計算
        Vector3 moveDirection = transform.position - lastPosition;

        // ある程度動いていれば方向を更新
        if (moveDirection.magnitude > 0.01f)
        {
            lastMoveDirection = moveDirection.normalized;
        }

        // 一定時間経過でオブジェクト生成
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObject(lastMoveDirection);
        }

        lastPosition = transform.position;
    }

    void SpawnObject(Vector3 direction)
    {
        if (objectToSpawn == null) return;

        // 生成位置（Y固定）
        Vector3 spawnPosition = new Vector3(
            transform.position.x + direction.x * spawnDistance,
            fixedYPosition,
            transform.position.z + direction.z * spawnDistance
        );

        // プレハブを生成
        GameObject spawned = Instantiate(objectToSpawn, spawnPosition, Quaternion.LookRotation(direction));

        // Rigidbody 設定（動かないように）
        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // Ground および Player タグとの衝突を無視（スクリプト内完結）
        string[] ignoreTags = { "Ground", "Player" };

        // 生成されたオブジェクトのすべてのColliderを取得
        Collider[] spawnedColliders = spawned.GetComponentsInChildren<Collider>(includeInactive: true);

        foreach (string tag in ignoreTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject target in targets)
            {
                Collider[] targetColliders = target.GetComponentsInChildren<Collider>(includeInactive: true);
                foreach (var sc in spawnedColliders)
                {
                    foreach (var tc in targetColliders)
                    {
                        if (sc != null && tc != null)
                        {
                            Physics.IgnoreCollision(sc, tc, true);
                        }
                    }
                }
            }
        }

        // 一定時間後に削除
        Destroy(spawned, lifeTime);
    }
}
