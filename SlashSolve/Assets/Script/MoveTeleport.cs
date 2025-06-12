using UnityEngine;

public class TeleportInArea : MonoBehaviour
{
    public float teleportInterval = 3f; // 瞬間移動の間隔（秒）
    public Vector2 areaMin = new Vector2(-10f, -10f); // 範囲の最小XZ座標
    public Vector2 areaMax = new Vector2(10f, 10f);   // 範囲の最大XZ座標
    public LayerMask groundLayer; // 地面のレイヤー

    private float timer;
    private float originalY;

    void Start()
    {
        timer = teleportInterval;
        originalY = transform.position.y;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Teleport();
            timer = teleportInterval;
        }
    }

    void Teleport()
    {
        for (int i = 0; i < 10; i++) // 最大10回試行
        {
            float randomX = Random.Range(areaMin.x, areaMax.x);
            float randomZ = Random.Range(areaMin.y, areaMax.y);
            Vector3 checkPosition = new Vector3(randomX, originalY + 10f, randomZ); // 少し上からRayを打つ

            if (Physics.Raycast(checkPosition, Vector3.down, out RaycastHit hit, 20f, groundLayer))
            {
                Vector3 newPosition = new Vector3(randomX, originalY, randomZ);
                transform.position = newPosition;
                return;
            }
        }

        Debug.LogWarning("地面が見つからなかったため、瞬間移動できませんでした。");
    }
}
