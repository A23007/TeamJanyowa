using TMPro;
using UnityEngine;

public class TagCount : MonoBehaviour
{
   
    public string targetTag = "Enemy"; // �^�O��
    public TextMeshProUGUI countText; // TextMeshPro��UI

    void Update()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);
        int count = taggedObjects.Length;
        countText.text = count.ToString();
    }
}
