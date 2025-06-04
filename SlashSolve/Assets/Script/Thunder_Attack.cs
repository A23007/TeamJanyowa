//雷の攻撃発生　増田

using UnityEngine;
using System.Collections.Generic;

public class Thunder_Attack : MonoBehaviour
{
    [Header("雷のプレハブ")]
    public GameObject thunderPrefab;

    [Header("生成間隔 (秒)")]
    public float spawnInterval = 1.0f;

    [Header("ヒット確率 (0〜1)")]
    [Range(0f, 1f)]
    public float hitProbability = 0.1f;

    [Header("生成範囲 (x, y, zの±範囲)")]
    public Vector3 spawnRange = new Vector3(5f, 0f, 5f);

    [Header("対象タグ")]
    public string[] targetTags = { "Enemy", "RockObject", "ScissorsObject", "PeparObject" };

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnThunder();
            timer = 0f;
        }
    }

    void SpawnThunder()
    {
        Vector3 spawnPosition;

        GameObject[] potentialTargets = GetTaggedObjectsInRange();

        bool tryHit = Random.value <= hitProbability && potentialTargets.Length > 0;

        if (tryHit)
        {
            GameObject target = potentialTargets[Random.Range(0, potentialTargets.Length)];
            spawnPosition = target.transform.position;
        }
        else
        {
            spawnPosition = GetRandomPosition();
        }

        GameObject thunder = Instantiate(thunderPrefab, spawnPosition, Quaternion.identity);
        Destroy(thunder, 5f);
    }

    Vector3 GetRandomPosition()
    {
        return transform.position + new Vector3(
            Random.Range(-spawnRange.x, spawnRange.x),
            Random.Range(-spawnRange.y, spawnRange.y),
            Random.Range(-spawnRange.z, spawnRange.z)
        );
    }

    GameObject[] GetTaggedObjectsInRange()
    {
        List<GameObject> result = new List<GameObject>();

        foreach (string tag in targetTags)
        {
            GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in found)
            {
                if (IsWithinRange(obj.transform.position))
                {
                    result.Add(obj);
                }
            }
        }

        return result.ToArray();
    }

    bool IsWithinRange(Vector3 position)
    {
        Vector3 relativePos = position - transform.position;

        return Mathf.Abs(relativePos.x) <= spawnRange.x &&
               Mathf.Abs(relativePos.y) <= spawnRange.y &&
               Mathf.Abs(relativePos.z) <= spawnRange.z;
    }
}
