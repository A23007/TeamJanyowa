//シーン遷移（GameScene）りとらいできないよおおお　増田

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
    private Button retryButton; // Retryボタン

    private const string PreviousSceneKey = "PreviousScene";

    void Start()
    {
        // 各ボタンにリスナー登録
        foreach (var sb in sceneButtons)
        {
            string targetScene = sb.sceneName;
            sb.button.onClick.AddListener(() =>
            {
                // 現在のシーンを保存
                PlayerPrefs.SetString(PreviousSceneKey, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                PlayerPrefs.Save();
                UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
            });
        }

        // Retryボタンの処理（前のシーンに戻る）
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
                    Debug.LogWarning("前のシーン情報が存在しません。");
                }
            });
        }
    }
}
