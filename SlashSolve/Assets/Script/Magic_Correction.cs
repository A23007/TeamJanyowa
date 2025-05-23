//���@�g���p�␳�@���c

using UnityEngine;

public class CenterObjectToCollider : MonoBehaviour
{
    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogWarning("Collider��������܂���");
            return;
        }

        // Collider�̒��S�i���[���h���W�j
        Vector3 colliderCenterWorld = col.bounds.center;

        // �����̌��݂̈ʒu�Ƃ̍���
        Vector3 offset = colliderCenterWorld - transform.position;

        // �S�Ă̎q���I�t�Z�b�g�ŋt�ɓ������i�����ڂ̕␳�j
        foreach (Transform child in transform)
        {
            child.position -= offset;
        }

        // ���g�̈ʒu��Collider���S��
        transform.position = colliderCenterWorld;
    }
}
