using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuPreviewScreenshot : MonoBehaviour
{
    private DisplayElements _displayElements;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _shareButton;
    [SerializeField] private Button _removeButton;


    // Start is called before the first frame update
    void Start()
    {
        _displayElements = GetComponent<DisplayElements>();
        InitButtons();
    }

    void InitButtons() {
        BackButtonClick();
        RemoveButtonClick();
        ShareButtonClick();
    }

    void BackButtonClick() {
        _backButton.onClick.AddListener(() => {
            _displayElements.HideElements();
        });
    }

    void RemoveButtonClick()
    {
        _removeButton.onClick.AddListener(() => {
            string filePath = Application.persistentDataPath + ScreenshotHandler.GALLERY_PATH + "/" + ScreenshotHandler.fileName;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _displayElements.HideElements();
                Recorder.ScreenRecorder.ShowToast("Screenshoot was deleted");
            }
        });
    }

    void ShareButtonClick()
    {
        _shareButton.onClick.AddListener(() =>
        {
            StartCoroutine(Share());
        });
    }
    IEnumerator Share() 
    {
        yield return new WaitForEndOfFrame();
        string filePath = Application.persistentDataPath + ScreenshotHandler.GALLERY_PATH + "/" + ScreenshotHandler.fileName;
        new NativeShare().AddFile(filePath)
                .SetSubject("AR-Project").SetText("Дополните окружение с помощью AR!!")
                .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
                .Share();
    }
}
