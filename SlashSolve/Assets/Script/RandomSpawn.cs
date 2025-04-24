//  �G�̐����@���c

using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [Header("��������I�u�W�F�N�g")]
    public GameObject prefab;

    [Header("�������ƂȂ��I�u�W�F�N�g")]
    public Transform targetObject;

    [Header("�����͈́i��I�u�W�F�N�g�̏�Ƀ����_���j")]
    public float spawnRadius = 2f;
    public float heightOffset = 2f;

    [Header("�����ݒ�")]
    public int spawnCountPerInterval = 3;      // ���̐�����
    public float spawnInterval = 2f;           // �����Ԋu
    public int maxSpawnCount = 1000;           // ���������i�C�ӂő傫�߂Ɂj

    [Header("�����ɑ��݂ł���ő吔")]
    public int maxSimultaneousObjects = 200;   // �C���X�y�N�^�[�Őݒ�

    private float timer = 0f;
    private int totalSpawned = 0;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void Update()
    {
        // ���X�g����null�i�j��ς݂̃I�u�W�F�N�g�j������
        spawnedObjects.RemoveAll(obj => obj == null);

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // �����������Ƒ��������Ɉ����������Ă��Ȃ���ΐ���
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


