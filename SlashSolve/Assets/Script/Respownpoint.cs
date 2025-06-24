using UnityEngine;

public class Respownpoint : MonoBehaviour
{
    void Update()
    {
        // YÀ•W‚ª-50‚æ‚è‰º‚É—‚¿‚½‚ç
        if (transform.position.y < -50f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = 5f; // YÀ•W‚ğ5‚É•ÏX
            transform.position = newPosition;
        }
    }
}
