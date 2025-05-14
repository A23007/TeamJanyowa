using TMPro;
using UnityEngine;

public class TagCount : MonoBehaviour
{
   
    public string targetTag = "Enemy"; // ƒ^ƒO–¼
    public TextMeshProUGUI countText; // TextMeshPro‚ÌUI

    void Update()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);
        int count = taggedObjects.Length;
        countText.text = count.ToString();
    }
}
