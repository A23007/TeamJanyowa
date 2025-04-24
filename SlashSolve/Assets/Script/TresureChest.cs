//宝箱の処理　増田

using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawnManager : MonoBehaviour
{
    public Button[] clickableButtons = new Button[3];
    public GameObject[] spawnPrefabs = new GameObject[3]; // ボタンごとに生成するPrefabを指定
    public GameObject canvasToShow;

    private bool isPaused = false;

    private void Start()
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false); // 最初は非表示
        }

        for (int i = 0; i < clickableButtons.Length; i++)
        {
            int index = i; // キャプチャ対策
            if (clickableButtons[i] != null)
            {
                clickableButtons[i].onClick.AddListener(() => OnButtonClicked(index));
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

    private void OnButtonClicked(int index)
    {
        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false);
        }

        Time.timeScale = 1f; // ゲーム再開
        isPaused = false;

        if (index >= 0 && index < spawnPrefabs.Length && spawnPrefabs[index] != null)
        {
            Instantiate(spawnPrefabs[index], transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // このオブジェクトを削除
    }
}
