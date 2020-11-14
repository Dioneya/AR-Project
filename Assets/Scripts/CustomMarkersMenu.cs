using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;
using UnityEditor;
using ZXing.Client.Result;

public class CustomMarkersMenu : MonoBehaviour
{
    public GameObject markerBtn;
    public GameObject objectBtn;
    public GameObject nextBtn;
    private Button marker;
    private Button object_;
    private Button next;

    private string markerPath = "";
    private string objectPath = "";
    void Start()
    {
        marker = markerBtn.GetComponent<Button>();
        object_ = objectBtn.GetComponent<Button>();
        next = nextBtn.GetComponent<Button>();

        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

        marker.onClick.AddListener(ChooseMaker);
        object_.onClick.AddListener(ChoseObject);
        next.onClick.AddListener(Next);
    }

    void OnEnable() 
    {
        markerPath = "";
        objectPath = "";
    }
    private void ChooseMaker() 
    {
        FileBrowser.SetDefaultFilter(".jpg");
        StartCoroutine(ShowLoadDialogCoroutineMarker(true));
    }

    private void ChoseObject() 
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Assets", ".asset",""), new FileBrowser.Filter("Videos", ".mp4"), new FileBrowser.Filter("Audio", ".mp3"));
        StartCoroutine(ShowLoadDialogCoroutineMarker(false));
    }

    IEnumerator ShowLoadDialogCoroutineMarker(bool isMarker)
    {
        yield return FileBrowser.WaitForLoadDialog(false, false, null, "Load File", "Load");

        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            InfoAdd(FileBrowser.Result[0], isMarker);
        }
    }

    void InfoAdd(string path,bool isMarker)
    {
        if(isMarker)
            markerPath = path;
        else
            objectPath = path;
    }

    void Next() 
    {
        if (markerPath != "" && objectPath != "") 
        {
            CustomMarker.markerPath = markerPath;
            CustomMarker.objectPath = objectPath;
            CustomMarker.isCustom = true;
            GlobalVariables.isInstChanged = true;
        }
    }
}
