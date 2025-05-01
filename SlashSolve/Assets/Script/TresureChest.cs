//�󔠂̏����@���c

using UnityEngine;
using UnityEngine.UI;

public class TresureChest : MonoBehaviour
{
    [Header("����UI�ݒ�")]
    public GameObject canvasToShow;            // UI�L�����o�X�i���ʁj
    public Button[] clickableButtons;          // �I�����{�^���i���ʁj
    public GameObject[] spawnPrefabs;          // �e�{�^���ɑΉ�����Prefab

    private static bool isUIOpen = false;      // UI���J���Ă��邩�ǂ����i���ʊǗ��j
    private static TresureChest currentCaller; // ��UI���Ăяo�����I�u�W�F�N�g

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false);
        }

        Time.timeScale = 1f; // �V�[���J�n���Ɏ��Ԃ����Z�b�g

        for (int i = 0; i < clickableButtons.Length; i++)
        {
            int index = i;
            clickableButtons[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUIOpen && other.CompareTag("Player"))
        {
            if (canvasToShow != null)
            {
                currentCaller = this; // ���G�ꂽ���̃I�u�W�F�N�g���L�^
                canvasToShow.SetActive(true);
                Time.timeScale = 0f;
                isUIOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isUIOpen && currentCaller == this && other.CompareTag("Player"))
        {
            if (canvasToShow != null)
            {
                canvasToShow.SetActive(false);
            }

            Time.timeScale = 1f;
            isUIOpen = false;
            currentCaller = null;
        }
    }

    private void OnButtonClicked(int index)
    {
        if (!isUIOpen) return;

        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false);
        }

        Time.timeScale = 1f;
        isUIOpen = false;

        if (index >= 0 && index < spawnPrefabs.Length && spawnPrefabs[index] != null)
        {
            Instantiate(spawnPrefabs[index], currentCaller.transform.position, Quaternion.identity);
        }

        Destroy(currentCaller.gameObject); // UI���Ăяo�����I�u�W�F�N�g�������폜
        currentCaller = null;
    }
}
