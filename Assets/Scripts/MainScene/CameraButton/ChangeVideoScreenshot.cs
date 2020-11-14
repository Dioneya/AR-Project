using UnityEngine;
using UnityEngine.UI;

public class ChangeVideoScreenshot : MonoBehaviour
{
    [SerializeField] private Button _camButton;
    public GameObject PreviewImage;
    public GameObject PreviewVideo;
    private DisplayElements _displayElements;
    private RecordSetup _recordSetup;
    private Markers _markers;

    private bool isScreenshotButton = true;
    private void Start() {
        _displayElements = GetComponent<DisplayElements>();
        _recordSetup = GetComponent<RecordSetup>();
        _markers = GetComponent<Markers>();

        SetScreenshootButton();
    }
    private void SetRecordButton()
    {
        _camButton.onClick.AddListener(() =>
        {
            _recordSetup.ToggleRecord();
            if (!_recordSetup.isRecording && transform.parent.gameObject.activeSelf == true)
            {
                PreviewVideo.transform.parent.gameObject.SetActive(true);
                PreviewVideo.SetActive(true);
            }
            _displayElements.ShowAndHideElements();
            _markers.ChangeInst();
        });
    }

    private void SetScreenshootButton()
    {
        _camButton.onClick.AddListener(() => { ScreenshotHandler.TakeScreenshot_FullScreen();
                                                if (transform.parent.gameObject.activeSelf == true)
                                                {
                                                    PreviewImage.transform.parent.gameObject.SetActive(true);
                                                    PreviewImage.SetActive(true);
                                                }
                                                _displayElements.ShowAndHideElements();
                                               _markers.ChangeInst();
                                               //_PreviewVideo.SetActive(false);
                                             });
    }

    public void ToggleButton() {
        _camButton.onClick.RemoveAllListeners();
        if (isScreenshotButton)
            SetRecordButton();
        else
            SetScreenshootButton();
        isScreenshotButton = !isScreenshotButton;
    }
}
