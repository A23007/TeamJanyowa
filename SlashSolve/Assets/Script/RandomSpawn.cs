using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [Header("生成するオブジェクト")]
    public GameObject prefab;

    [Header("生成元となる基準オブジェクト")]
    public Transform targetObject;

    [Header("生成範囲（基準オブジェクトの上にランダム）")]
    public float spawnRadius = 2f;
    public float heightOffset = 2f;

    [Header("生成設定")]
    public int spawnCountPerInterval = 3; // 一回の生成数
    public float spawnInterval = 2f;       // 生成間隔
    public int maxSpawnCount = 20;         // 総生成数の上限

    private float timer = 0f;
    private int totalSpawned = 0; // 今まで生成した数

    void Update()
    {
        if (totalSpawned >= maxSpawnCount) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObjects();
        }
    }

    void SpawnObjects()
    {
        int remainingToSpawn = maxSpawnCount - totalSpawned;
        int spawnThisTime = Mathf.Min(spawnCountPerInterval, remainingToSpawn);

        for (int i = 0; i < spawnThisTime; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(
                targetObject.position.x + randomOffset.x,
                targetObject.position.y + heightOffset,
                targetObject.position.z + randomOffset.y
            );

            Instantiate(prefab, spawnPos, Quaternion.identity);
            totalSpawned++;
        }
    }
}
