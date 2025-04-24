// �G�t�F�N�g���v���C���[�ɍ��킹�ē����悤�ɂ���@���c


using UnityEngine;

public class Effect_Movement : MonoBehaviour
{
    [Tooltip("�Ǐ]���������^�[�Q�b�g�I�u�W�F�N�g")]
    public Transform target;

    [Tooltip("�^�[�Q�b�g����̑��Έʒu�i���[�J�����W�j")]
    public Vector3 offset = Vector3.zero;

    void Start()
    {
        // Player�^�O�����I�u�W�F�N�g��T����target�ɐݒ�
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player�^�O�����I�u�W�F�N�g��������܂���ł����B");
        }
    }

    void Update()
    {
        if (target != null)
        {
            // �^�[�Q�b�g�̃��[�J�������ɃI�t�Z�b�g��������
            transform.position = target.position + target.rotation * offset;
            transform.rotation = target.rotation;
        }
    }
}
