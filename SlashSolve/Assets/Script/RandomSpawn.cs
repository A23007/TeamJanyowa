//  �G�̐����@���c

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject prefab;
    [Range(0f, 1f)] public float spawnProbability = 1f; // �o���m��
    public int maxSpawnCount = 1000;                   // �ʂ̍ő�o����
    [HideInInspector] public int currentCount = 0;     // ���݂̐�����

    [Header("�������̍����I�t�Z�b�g")]
    public float heightOffset = 2f;                    // �ʂ̍����ݒ�
}

public class RandomSpawn : MonoBehaviour
{
    [Header("��������I�u�W�F�N�g�i�m���E�ʐ�������j")]
    public SpawnableObject[] spawnables;

    [Header("�������ƂȂ��I�u�W�F�N�g")]
    public Transform targetObject;

    [Header("�����͈́i��I�u�W�F�N�g�̏�Ƀ����_���j")]
    public float spawnRadius = 2f;

    [Header("�����ݒ�")]
    public int spawnCountPerInterval = 3;
    public float spawnInterval = 2f;
    public int totalMaxSpawnCount = 1000;

    [Header("�����ɑ��݂ł���ő吔")]
    public int maxSimultaneousObjects = 200;

    private float timer = 0f;
    private int totalSpawned = 0;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        spawnedObjects.RemoveAll(obj => obj == null);
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            if (spawnedObjects.Count < maxSimultaneousObjects &&
                totalSpawned < totalMaxSpawnCount &&
                IsPlayerInOwnRange())
            {
                SpawnObjects();
            }
        }
    }

    void SpawnObjects()
    {
        int remainingToSpawn = totalMaxSpawnCount - totalSpawned;
        int availableSlots = maxSimultaneousObjects - spawnedObjects.Count;
        int spawnThisTime = Mathf.Min(spawnCountPerInterval, remainingToSpawn, availableSlots);

        for (int i = 0; i < spawnThisTime; i++)
        {
            SpawnableObject selected = ChooseRandomSpawnable();
            if (selected == null) break;

            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(
                targetObject.position.x + randomOffset.x,
                targetObject.position.y + selected.heightOffset, // �ʂ̍������g�p
                targetObject.position.z + randomOffset.y
            );

            GameObject obj = Instantiate(selected.prefab, spawnPos, Quaternion.identity);
            spawnedObjects.Add(obj);

            selected.currentCount++;
            totalSpawned++;
        }
    }

    SpawnableObject ChooseRandomSpawnable()
    {
        List<SpawnableObject> validCandidates = new List<SpawnableObject>();

        foreach (var s in spawnables)
        {
            if (s.currentCount < s.maxSpawnCount && Random.value < s.spawnProbability)
            {
                validCandidates.Add(s);
            }
        }

        if (validCandidates.Count == 0) return null;

        return validCandidates[Random.Range(0, validCandidates.Count)];
    }

    bool IsPlayerInOwnRange()
    {
        Vector3 center = targetObject.position + Vector3.up * 2f; // ���ʍ����Ŕ���
        Collider[] hits = Physics.OverlapSphere(center, spawnRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                float dist = Vector3.Distance(hit.transform.position, center);
                if (dist <= spawnRadius)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(targetObject.position + Vector3.up * 2f, spawnRadius);
        }
    }
}

