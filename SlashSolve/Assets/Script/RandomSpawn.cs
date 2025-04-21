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
    public int spawnCountPerInterval = 3; // ���̐�����
    public float spawnInterval = 2f;       // �����Ԋu
    public int maxSpawnCount = 20;         // ���������̏��

    private float timer = 0f;
    private int totalSpawned = 0; // ���܂Ő���������

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
