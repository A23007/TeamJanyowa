//マップの自動生成（暫定版）　増田

using UnityEngine;

public class Random_Map_Spawn : MonoBehaviour
{
    // 生成するオブジェクトのプレハブをInspectorから登録
    public GameObject[] spawnPrefabs;

    // 生成位置
    public Transform spawnPoint;

    // ステージの回転角（Inspectorから指定）
    public Vector3 spawnRotationEuler;

    void Start()
    {
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        if (spawnPrefabs.Length == 0) return;

        // ランダムにインデックスを取得
        int index = Random.Range(0, spawnPrefabs.Length);

        // 指定の角度をQuaternionに変換
        Quaternion spawnRotation = Quaternion.Euler(spawnRotationEuler);

        // オブジェクトを生成（指定の位置と角度で）
        Instantiate(spawnPrefabs[index], spawnPoint.position, spawnRotation);
    }
}
