using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;

public class TextDownload : MonoBehaviour, IDownloadeble
{
    public string text;
    private GameObject Text;

    public void StartDownload(string filePath, DataSetTrackableBehaviour trackableBehaviour)
    {
        Transform textObj = ((GameObject)Instantiate(Resources.Load("Prefabs/TextCanvas"))).transform;
        textObj.SetParent(transform);

        for (int i = 0; i < textObj.childCount; i++) {
            if (textObj.GetChild(i).name == "TextContent") 
            {
                Transform TextContent = textObj.GetChild(i);
                Text = TextContent.GetChild(0).gameObject;
                break;
            }
        }

        Text.GetComponent<TextMeshProUGUI>().text = text;
    }
}
