//�J�����̓����@���c

using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // �ǂ�������^�[�Q�b�g
    public float distance = 5.0f; // �^�[�Q�b�g�Ƃ̋���
    public float height = 3.0f; // �J�����̍���
    public float rotationSpeed = 90.0f; // �J�����̉�]���x�i�x/�b�j
    public float smoothSpeed = 5.0f; // �X���[�Y���̋����i�傫���قǑ����Ǐ]�j
    public float initialRotation = 0f; // �����J������]�p�x�iY���j

    private float currentRotation;

    void Start()
    {
        currentRotation = initialRotation;
    }

    void LateUpdate()
    {
        HandleRotationInput();

        // ��]�Ɋ�Â��I�t�Z�b�g���v�Z
        Quaternion rotation = Quaternion.Euler(0, currentRotation, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        // �^�[�Q�b�g�ʒu����J�����̗��z�ʒu���v�Z
        Vector3 desiredPosition = target.position + Vector3.up * height + offset;

        // ���݂̈ʒu���痝�z�ʒu�֊��炩�ɕ��
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // �J�����̉�]���^�[�Q�b�g�����֊��炩�ɕ��
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentRotation -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentRotation += rotationSpeed * Time.deltaTime;
        }
    }
}

