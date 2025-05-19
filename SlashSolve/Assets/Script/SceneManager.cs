//シーン遷移（GameScene）　増田

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MultiSceneChanger : MonoBehaviour
{
    [System.Serializable]
    public class SceneButton
    {
        public Button button;       // このボタンが押されたら
        public string sceneName;    // このシーンに移動
    }

    [SerializeField]
    private List<SceneButton> sceneButtons = new List<SceneButton>();

    void Start()
    {
        foreach (var sb in sceneButtons)
        {
            string targetScene = sb.sceneName;  // クロージャ対策
            sb.button.onClick.AddListener(() => SceneManager.LoadScene(targetScene));
        }
    }
}
