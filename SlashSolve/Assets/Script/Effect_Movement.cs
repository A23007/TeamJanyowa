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

    [Header("����ݒ�")]
    [Tooltip("Player�̎������邩�ǂ���")]
    public bool orbitAroundTarget = false;

    [Tooltip("��]���a�iXZ���ʁj")]
    public float orbitRadius = 2f;

    [Tooltip("��]���x�i�x/�b�j")]
    public float orbitSpeed = 90f;

    private float currentAngle = 0f;

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

        // �����p�x�̌v�Z�i�I�t�Z�b�g�����Ɂj
        if (orbitAroundTarget)
        {
            Vector3 offsetXZ = new Vector3(offset.x, 0, offset.z);
            if (offsetXZ.magnitude > 0f)
            {
                currentAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            if (orbitAroundTarget)
            {
                // ���Ԍo�߂Ŋp�x���X�V
                currentAngle += orbitSpeed * Time.deltaTime;
                if (currentAngle > 360f) currentAngle -= 360f;

                // �V�����ʒu���v�Z�iXZ���ʂŉ~��`���j
                float rad = currentAngle * Mathf.Deg2Rad;
                Vector3 orbitOffset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * orbitRadius;

                // �^�[�Q�b�g�ʒu�ɃI�t�Z�b�g��������iY���̍����� offset.y ���g�p�j
                transform.position = target.position + new Vector3(orbitOffset.x, offset.y, orbitOffset.z);
            }
            else
            {
                // �^�[�Q�b�g�̈ʒu�ɌŒ�I�t�Z�b�g�����Z
                transform.position = target.position + offset;
            }

            if (useRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
