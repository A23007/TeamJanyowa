//  敵の生成　増田

using System.Collections.Generic;
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
    public int spawnCountPerInterval = 3;      // 一回の生成数
    public float spawnInterval = 2f;           // 生成間隔
    public int maxSpawnCount = 1000;           // 総生成数（任意で大きめに）

    [Header("同時に存在できる最大数")]
    public int maxSimultaneousObjects = 200;   // インスペクターで設定

    private float timer = 0f;
    private int totalSpawned = 0;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        // リストからnull（破壊済みのオブジェクト）を除去
        spawnedObjects.RemoveAll(obj => obj == null);

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // 同時数制限と総数制限に引っかかっていなければ生成
            if (spawnedObjects.Count < maxSimultaneousObjects && totalSpawned < maxSpawnCount)
            {
                SpawnObjects();
            }
        }
    }

    void SpawnObjects()
    {
        int remainingToSpawn = maxSpawnCount - totalSpawned;
        int availableSlots = maxSimultaneousObjects - spawnedObjects.Count;
        int spawnThisTime = Mathf.Min(spawnCountPerInterval, remainingToSpawn, availableSlots);

        for (int i = 0; i < spawnThisTime; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(
                targetObject.position.x + randomOffset.x,
                targetObject.position.y + heightOffset,
                targetObject.position.z + randomOffset.y
            );

            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            spawnedObjects.Add(obj);
            totalSpawned++;
        }
    }
}


