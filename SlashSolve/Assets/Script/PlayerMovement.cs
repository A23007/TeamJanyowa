//Player�̓����𐧌䂷��X�N���v�g

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // �v���C���[�̈ړ����x
    public float stopDistance = 0.1f;   // �\���߂Â������~���邽�߂̂������l

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;  // �K�v�ɉ����ďd�͖�����
        rb.constraints = RigidbodyConstraints.FreezeRotation;  // ��]���Œ�
    }

    void Update()
    {
        // �}�E�X�J�[�\������Ray���΂�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // �n�ʂȂǂ�Collider��Ray�����������ꍇ
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = (targetPosition - transform.position);
            direction.y = 0f; // ���������̈ړ��𖳎�

            if (direction.magnitude > stopDistance)
            {
                Vector3 move = direction.normalized * moveSpeed * Time.deltaTime;
                transform.position += move;

                // �v���C���[���}�E�X�����Ɍ�����i�C�Ӂj
                if (move != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(move);
                }
            }
        }
    }
}
