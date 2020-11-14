using UnityEngine;
using UnityEngine.UI;

public class CameraButtonSwapImages : MonoBehaviour
{

    [SerializeField] private GameObject _CameraButton;
    [SerializeField] private ChangeVideoScreenshot _changeVideoScreenshoot;
    private Image _CurrentElementImage;

    private Image _camImg;

    void Start()
    {
        _CurrentElementImage = GetComponent<Image>();
        _camImg = _CameraButton.transform.GetChild(0).GetComponent<Image>();

        GetComponent<Button>().onClick.AddListener(() => SwapImages());
    }

    public void SwapImages() {
        Sprite _tempImage = _camImg.sprite;
        _camImg.sprite = _CurrentElementImage.sprite;
        _CurrentElementImage.sprite = _tempImage;
        _changeVideoScreenshoot.ToggleButton();
    }
}
