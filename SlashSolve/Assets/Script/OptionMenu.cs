//オプション・ミッションなど表示 増田

using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [Header("ESCキーで開くオプションメニュー")]
    public GameObject optionCanvas;

    [Header("Spaceキーで開くスペシャルメニュー（時間停止なし）")]
    public GameObject spaceCanvas;

    private bool isOptionOpen = false;
    private bool isSpaceOpen = false;

    void Start()
    {
        if (optionCanvas != null)
            optionCanvas.SetActive(false);

        if (spaceCanvas != null)
            spaceCanvas.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSpaceMenu();
        }
    }

    private void ToggleOptionMenu()
    {
        if (optionCanvas == null) return;

        isOptionOpen = !isOptionOpen;
        optionCanvas.SetActive(isOptionOpen);

        // Optionメニューが開いているときのみ時間停止
        Time.timeScale = isOptionOpen ? 0f : 1f;
    }

    private void ToggleSpaceMenu()
    {
        if (spaceCanvas == null) return;

        isSpaceOpen = !isSpaceOpen;
        spaceCanvas.SetActive(isSpaceOpen);

        // Spaceメニューは時間に影響を与えない（何もしない）
    }
}
