//�_���[�W����Enemy�@���c
using UnityEngine;

public class Effect_Hit : MonoBehaviour
{
    private EnemyStatus enemyStatus;

    void Start()
    {
        // �����I�u�W�F�N�g�ɃA�^�b�`����Ă��� EnemyStatus ���擾
        enemyStatus = GetComponent<EnemyStatus>();

        if (enemyStatus == null)
        {
            Debug.LogWarning("EnemyStatus ��������܂���ł����B");
        }
    }

    public void TakeDamage(int amount)
    {
        if (enemyStatus != null)
        {
            enemyStatus.TakeDamage(amount);  // EnemyStatus �Ƀ_���[�W������C����
        }
        else
        {
            Debug.LogWarning("EnemyStatus �����ݒ�̂��߃_���[�W�������ł��܂���B");
        }
    }
}
