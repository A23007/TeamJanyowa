//�V�[���J�ځiGameScene�j��Ƃ炢�ł��Ȃ��您�����@���c

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneButton
    {
        public Button button;
        public string sceneName;
    }

    [SerializeField]
    private List<SceneButton> sceneButtons = new List<SceneButton>();

    [SerializeField]
    private Button retryButton; // Retry�{�^��

    private const string PreviousSceneKey = "PreviousScene";

    void Start()
    {
        // �e�{�^���Ƀ��X�i�[�o�^
        foreach (var sb in sceneButtons)
        {
            string targetScene = sb.sceneName;
            sb.button.onClick.AddListener(() =>
            {
                // ���݂̃V�[����ۑ�
                PlayerPrefs.SetString(PreviousSceneKey, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                PlayerPrefs.Save();
                UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
            });
        }

        // Retry�{�^���̏����i�O�̃V�[���ɖ߂�j
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(() =>
            {
                string prevScene = PlayerPrefs.GetString(PreviousSceneKey, "");
                if (!string.IsNullOrEmpty(prevScene))
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(prevScene);
                }
                else
                {
                    Debug.LogWarning("�O�̃V�[����񂪑��݂��܂���B");
                }
            });
        }
    }
}
