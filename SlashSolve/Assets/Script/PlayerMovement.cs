//Player�̓����𐧌䂷��X�N���v�g

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // �ړ����x
    public float bounceForce = 0f;  // �Փˎ��̒��˕Ԃ�̗�

    private Rigidbody rb;  // Rigidbody���L���b�V��

    void Start()
    {
        // Rigidbody�R���|�[�l���g���擾
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            // Rigidbody���Ȃ��ꍇ�͒ǉ�
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // ���͂��`�F�b�N���Ĉړ�����������
        float horizontal = 0f;  // ���E�ړ� (A�AD)
        float vertical = 0f;    // �O��ړ� (W�AS)

        // A�L�[�ō��Ɉړ�
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }
        // D�L�[�ŉE�Ɉړ�
        if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }
        // W�L�[�őO�Ɉړ�
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }
        // S�L�[�Ō��Ɉړ�
        if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        // �ړ��x�N�g�����쐬
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        // �ړ�����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


}
