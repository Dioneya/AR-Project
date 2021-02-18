using TMPro;
using UnityEngine;
using Vuforia;

public class TextDownload : DownloadebleARObject
{
    private GameObject Text;
    public override void Download(DataSetTrackableBehaviour trackableBehaviour)
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

        Text.GetComponent<TextMeshProUGUI>().text = JSON.object_settings.text;
    }
}
