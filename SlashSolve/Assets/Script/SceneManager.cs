//�V�[���J�ځiGameScene�j�@���c

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiSceneChanger : MonoBehaviour
{
    [System.Serializable]
    public class SceneButton
    {
        public Button button;       // ���̃{�^���������ꂽ��
        public string sceneName;    // ���̃V�[���Ɉړ�
    }

    [SerializeField]
    private List<SceneButton> sceneButtons = new List<SceneButton>();

    void Start()
    {
        foreach (var sb in sceneButtons)
        {
            string targetScene = sb.sceneName;  // �N���[�W���΍�
            sb.button.onClick.AddListener(() => SceneManager.LoadScene(targetScene));
        }
    }
}
