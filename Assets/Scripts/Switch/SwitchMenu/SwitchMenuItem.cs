using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SwitchMenuItem : MonoBehaviour, IPointerClickHandler, ISwitchableItem
{
    public SwitchMenu _switchpages;

    public GameObject page;
    public Image image;
    [HideInInspector]
    public TextMeshProUGUI text;

    GameObject ISwitchableItem.page => page;
    Image ISwitchableItem.image => image;
    TextMeshProUGUI ISwitchableItem.text => text;

    public void Start()
    {
        _switchpages.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _switchpages.ChangeActiveItems(this);
    }
}
