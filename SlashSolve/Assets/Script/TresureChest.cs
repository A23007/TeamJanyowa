//宝箱の処理　増田

using UnityEngine;
using UnityEngine.UI;

public class PauseOnTouchUI : MonoBehaviour
{
    [Header("表示するキャンバス")]
    public GameObject canvasToShow;

    [Header("クリック対象のボタン（最大3つ）")]
    public Button[] clickableButtons = new Button[3];

    private bool isPaused = false;

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false); // 最初は非表示
        }

        // 全てのボタンにクリックリスナーを登録
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
                Time.timeScale = 0f; // ゲーム停止
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

        Time.timeScale = 1f; // ゲーム再開
        isPaused = false;

        Destroy(gameObject); // このオブジェクトを削除
    }
}
