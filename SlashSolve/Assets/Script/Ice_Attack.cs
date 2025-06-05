


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

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObject();
        }

        lastPosition = transform.position;
    }

    void SpawnObject()
    {
        if (objectToSpawn == null) return;

        // 移動方向の取得（停止中は右向き）
        Vector3 moveDirection = (transform.position - lastPosition).normalized;
        if (moveDirection == Vector3.zero) moveDirection = Vector3.right;

        // 生成位置（Y固定）
        Vector3 spawnPosition = new Vector3(
            transform.position.x + moveDirection.x * spawnDistance,
            fixedYPosition,
            transform.position.z + moveDirection.z * spawnDistance
        );

        // 生成（向きは移動方向）
        GameObject spawned = Instantiate(objectToSpawn, spawnPosition, Quaternion.LookRotation(moveDirection));

        // Rigidbody を持っていれば isKinematic にして動かないようにする
        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;     // 物理で動かない
            rb.useGravity = false;     // 重力無効（必要なら）
        }

        // Ground および Player タグとの衝突を無視
        string[] ignoreTags = { "Ground", "Player" };
        var ignoreColliders = ignoreTags
            .SelectMany(tag => GameObject.FindGameObjectsWithTag(tag))
            .SelectMany(go => go.GetComponentsInChildren<Collider>())
            .ToArray();

        Collider[] spawnedColliders = spawned.GetComponentsInChildren<Collider>();

        foreach (var ignoreCol in ignoreColliders)
        {
            foreach (var spawnCol in spawnedColliders)
            {
                Physics.IgnoreCollision(spawnCol, ignoreCol);
            }
        }

        // 一定時間後に削除
        Destroy(spawned, lifeTime);
    }
}
