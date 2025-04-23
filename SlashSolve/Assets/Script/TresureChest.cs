//�󔠂̏����@���c

using UnityEngine;
using UnityEngine.UI;

public class PauseOnTouchUI : MonoBehaviour
{
    [Header("�\������L�����o�X")]
    public GameObject canvasToShow;

    [Header("�N���b�N�Ώۂ̃{�^���i�ő�3�j")]
    public Button[] clickableButtons = new Button[3];

    private bool isPaused = false;

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false); // �ŏ��͔�\��
        }

        // �S�Ẵ{�^���ɃN���b�N���X�i�[��o�^
        foreach (Button btn in clickableButtons)
        {
            if (btn != null)
            {
                btn.onClick.AddListener(OnAnyButtonClicked);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPaused && other.CompareTag("Player"))
        {
            if (canvasToShow != null)
            {
                canvasToShow.SetActive(true);
                Time.timeScale = 0f; // �Q�[����~
                isPaused = true;
            }
        }
    }

    private void OnAnyButtonClicked()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false);
        }

        Time.timeScale = 1f; // �Q�[���ĊJ
        isPaused = false;

        Destroy(gameObject); // ���̃I�u�W�F�N�g���폜
    }
}
