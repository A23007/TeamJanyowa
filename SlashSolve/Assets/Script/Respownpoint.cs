using UnityEngine;

public class Respownpoint : MonoBehaviour
{
    void Update()
    {
        // Y���W��-50��艺�ɗ�������
        if (transform.position.y < -50f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = 5f; // Y���W��5�ɕύX
            transform.position = newPosition;
        }
    }
}
