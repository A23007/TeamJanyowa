//マップの自動生成（暫定版）　増田

using UnityEngine;

public class Random_Map_Spawn : MonoBehaviour
{
    // 生成するオブジェクトのプレハブをInspectorから登録
    public GameObject[] spawnPrefabs;

    // 生成位置
    public Transform spawnPoint;

    void Start()
    {
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        if (spawnPrefabs.Length == 0) return;

        // ランダムにインデックスを取得
        int index = Random.Range(0, spawnPrefabs.Length);

        // オブジェクトを生成
        Instantiate(spawnPrefabs[index], spawnPoint.position, Quaternion.identity);
    }
}


