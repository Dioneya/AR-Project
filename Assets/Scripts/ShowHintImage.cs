using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowHintImage : MonoBehaviour
{
    public GameObject HintBlock;
    public RawImage HintImage;
    public TextMeshProUGUI HintDescription;
    [HideInInspector] public string text;
    private bool isShowed = false;
    // Start is called before the first frame update
    public void DisplaySticker() {
        RawImage image = GetComponent<RawImage>();
        if (image != null)
            HintImage.texture = GetComponent<RawImage>().texture;

        HintDescription.text = text;

        if (isShowed == false)
        {
            HintBlock.SetActive(true);
            isShowed = true;
        }
        else
        {
            HintBlock.SetActive(false);
            isShowed = false;
        }
    }

    public void OnMouseDown()
    {
        DisplaySticker();
        
    }
    public void OnMouseUp()
    {
        DisplaySticker();
    }
}
