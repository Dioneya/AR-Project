using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public interface ISwitchable {
    void Subscribe(ISwitchableItem switchPageItem);
    void ChangePage(ISwitchableItem switchPageItem);
}

public class SwitchPages : MonoBehaviour, ISwitchable
{
    [SerializeField] protected MenuPages menuPages;
    public List<ISwitchableItem> _switchPagesItems;
    public Color32 _ActiveColor;
    public Color32 _DefaultColor;
    protected ISwitchableItem _selectedPagesItem;

    public void Subscribe(ISwitchableItem switchPageItem) {
        if (_switchPagesItems == null) {
            _switchPagesItems = new List<ISwitchableItem>();
        }

        _switchPagesItems.Add(switchPageItem);
    }

    public void ChangePage(ISwitchableItem switchPageItem) {
        ChangeActiveItems(switchPageItem);
        menuPages?.MoveCamera(switchPageItem.page.transform);
    }

    public void ChangeActiveItems(ISwitchableItem switchPageItem) {
        _selectedPagesItem = switchPageItem;
        ResetTabs();
        ChangeElementsColor(_selectedPagesItem, _ActiveColor);
        _selectedPagesItem.page.SetActive(true);
    }
    private void ResetTabs() {
        foreach (ISwitchableItem item in _switchPagesItems) {
            ChangeElementsColor(item, _DefaultColor);
            item.page.SetActive(false);
        }
    }

    private void ChangeElementsColor(ISwitchableItem item, Color32 color) {
        if (item?.image != null)
            item.image.color = color;
        if (item?.text != null)
            item.text.color = color;
    }
}
