// �G�t�F�N�g���v���C���[�ɍ��킹�ē����悤�ɂ���@���c


using UnityEngine;

public class Effect_Movement : MonoBehaviour
{
    [Tooltip("�Ǐ]���������^�[�Q�b�g�I�u�W�F�N�g")]
    public Transform target;

    [Tooltip("�^�[�Q�b�g����̑��Έʒu�i���[���h��ԁj")]
    public Vector3 offset = Vector3.zero;

    [Tooltip("�^�[�Q�b�g�̉�]�ɒǏ]���邩�ǂ���")]
    public bool useRotation = false;

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
            // �^�[�Q�b�g�̈ʒu�ɁA���[���h��Ԃł̌Œ�I�t�Z�b�g�����Z
            transform.position = target.position + offset;

            if (useRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
