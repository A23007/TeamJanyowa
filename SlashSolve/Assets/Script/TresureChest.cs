//宝箱の処理　増田

using UnityEngine;
using UnityEngine.UI;

public class TresureChest : MonoBehaviour
{
    [Header("共通UI設定")]
    public GameObject canvasToShow;            // UIキャンバス（共通）
    public Button[] clickableButtons;          // 選択肢ボタン（共通）
    public GameObject[] spawnPrefabs;          // 各ボタンに対応したPrefab

    private static bool isUIOpen = false;      // UIが開いているかどうか（共通管理）
    private static TresureChest currentCaller; // 今UIを呼び出したオブジェクト

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false);
        }

        Time.timeScale = 1f; // シーン開始時に時間をリセット

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
                currentCaller = this; // 今触れたこのオブジェクトを記録
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

        Destroy(currentCaller.gameObject); // UIを呼び出したオブジェクトだけを削除
        currentCaller = null;
    }
}
