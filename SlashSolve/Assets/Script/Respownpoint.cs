using UnityEngine;

public class Respownpoint : MonoBehaviour
{
    void Update()
    {
        // Y座標が-50より下に落ちたら
        if (transform.position.y < -50f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = 5f; // Y座標を5に変更
            transform.position = newPosition;
        }
    }
}
