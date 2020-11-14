using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public interface ISwitchableItem {
    GameObject page { get; }
    Image image { get; }
    TextMeshProUGUI text { get; }

    void Start();
    void OnPointerClick(PointerEventData eventData);
}

public class SwitchPageItem : MonoBehaviour, IPointerClickHandler, ISwitchableItem
{
    [SerializeField] private SwitchPages _switchpages;

    public GameObject page;
    public Image image;
    public TextMeshProUGUI text;

    [HideInInspector]
    public Button button;

    Image ISwitchableItem.image { get => image; }
    GameObject ISwitchableItem.page => page;
    TextMeshProUGUI ISwitchableItem.text => text;

    public void Start()
    {
        button = GetComponent<Button>();
        _switchpages.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _switchpages.ChangePage(this);
    }
}
