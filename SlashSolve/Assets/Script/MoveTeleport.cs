using UnityEngine;

public class TeleportInArea : MonoBehaviour
{
    public float teleportInterval = 3f; // �u�Ԉړ��̊Ԋu�i�b�j
    public Vector2 areaMin = new Vector2(-10f, -10f); // �͈͂̍ŏ�XZ���W
    public Vector2 areaMax = new Vector2(10f, 10f);   // �͈͂̍ő�XZ���W
    public LayerMask groundLayer; // �n�ʂ̃��C���[

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
        for (int i = 0; i < 10; i++) // �ő�10�񎎍s
        {
            float randomX = Random.Range(areaMin.x, areaMax.x);
            float randomZ = Random.Range(areaMin.y, areaMax.y);
            Vector3 checkPosition = new Vector3(randomX, originalY + 10f, randomZ); // �����ォ��Ray��ł�

            if (Physics.Raycast(checkPosition, Vector3.down, out RaycastHit hit, 20f, groundLayer))
            {
                Vector3 newPosition = new Vector3(randomX, originalY, randomZ);
                transform.position = newPosition;
                return;
            }
        }

        Debug.LogWarning("�n�ʂ�������Ȃ��������߁A�u�Ԉړ��ł��܂���ł����B");
    }
}
