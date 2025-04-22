//�J�����̓����@���c

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // �ǂ�������^�[�Q�b�g�i�Ⴆ�΁A�v���C���[�j
    public float distance = 5.0f; // �^�[�Q�b�g�Ƃ̋���
    public float height = 3.0f; // �����i�J������Y���j
    public float rotationSpeed = 3.0f; // �J�����̉�]���x
    public float smoothSpeed = 0.125f; // �X���[�Y�ɓ��������߂̕�ԑ��x

    private float currentRotation = 0f; // �J�����̌��݂̉�]�p�x

    void Update()
    {
        // ���L�[�ŃJ�����̉�]��ύX
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }

        // �^�[�Q�b�g�̌��Ɉʒu��ݒ�
        Vector3 targetPosition = target.position + new Vector3(0, height, 0); // �^�[�Q�b�g�̍����𒲐�

        // ��]���l�������I�t�Z�b�g���v�Z
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance); // �J�����̌��ɔz�u

        // �X���[�Y�ɃJ�����̈ʒu���X�V
        Vector3 desiredPosition = targetPosition + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // �X���[�Y�ɃJ�������^�[�Q�b�g�������悤�ɂ���
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
    }
}
