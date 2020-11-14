using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class SwitchTextItem : MonoBehaviour, IPointerClickHandler, ISwitchableItem
{
    [SerializeField] private SwitchTextPages _switchpages;

    public GameObject page;
    public TextMeshProUGUI text;
    [HideInInspector] public Image image => null;

    [HideInInspector]
    public Button button;

    GameObject ISwitchableItem.page => page;
    TextMeshProUGUI ISwitchableItem.text => text;

    public void Start()
    {
        button = GetComponent<Button>();
        _switchpages.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _switchpages.OnTabSelected(this);
    }
}
