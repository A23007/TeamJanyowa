//氷攻撃　増田


// 氷攻撃　増田（Player中心高さ+1で生成）

using UnityEngine;
using System.Linq;

public class Ice_Attack : MonoBehaviour
{
    public GameObject objectToSpawn;     // 生成するプレハブ
    public Transform target;             // 追従対象（例: Player）を外部から指定可能
    public float spawnInterval = 1.0f;   // 生成間隔（秒）
    public float spawnDistance = 2.0f;   // 生成距離
    public float lifeTime = 5.0f;        // 存続時間

    private float timer = 0f;
    private Vector3 lastPosition;
    private Vector3 lastMoveDirection = Vector3.right;  // 初期方向は右向き

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("Playerタグのオブジェクトが見つかりませんでした。Ice_Attack の target を手動で設定してください。");
                enabled = false;
                return;
            }
        }

        lastPosition = target.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        Vector3 moveDirection = target.position - lastPosition;

        if (moveDirection.magnitude > 0.01f)
        {
            lastMoveDirection = moveDirection.normalized;
        }

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObject(lastMoveDirection);
        }

        lastPosition = target.position;
    }

    void SpawnObject(Vector3 direction)
    {
        if (objectToSpawn == null || target == null) return;

        // 高さを target の中心 + 1 に設定
        float y = target.position.y;
        Renderer renderer = target.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            y = renderer.bounds.center.y + 0f;
        }

        Vector3 spawnPosition = new Vector3(
            target.position.x + direction.x * spawnDistance,
            y,
            target.position.z + direction.z * spawnDistance
        );

        GameObject spawned = Instantiate(objectToSpawn, spawnPosition, Quaternion.LookRotation(direction));

        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        string[] ignoreTags = { "Ground", "Player" };
        Collider[] spawnedColliders = spawned.GetComponentsInChildren<Collider>(includeInactive: true);

        foreach (string tag in ignoreTags)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject targetObj in targets)
            {
                Collider[] targetColliders = targetObj.GetComponentsInChildren<Collider>(includeInactive: true);
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

        Destroy(spawned, lifeTime);
    }
}
