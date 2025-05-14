//�}�b�v�̎��������i�b��Łj�@���c

using UnityEngine;

public class Random_Map_Spawn : MonoBehaviour
{
    // ��������I�u�W�F�N�g�̃v���n�u��Inspector����o�^
    public GameObject[] spawnPrefabs;

    // �����ʒu
    public Transform spawnPoint;

    // �X�e�[�W�̉�]�p�iInspector����w��j
    public Vector3 spawnRotationEuler;

    void Start()
    {
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        if (spawnPrefabs.Length == 0) return;

        // �����_���ɃC���f�b�N�X���擾
        int index = Random.Range(0, spawnPrefabs.Length);

        // �w��̊p�x��Quaternion�ɕϊ�
        Quaternion spawnRotation = Quaternion.Euler(spawnRotationEuler);

        // �I�u�W�F�N�g�𐶐��i�w��̈ʒu�Ɗp�x�Łj
        Instantiate(spawnPrefabs[index], spawnPoint.position, spawnRotation);
    }
}
