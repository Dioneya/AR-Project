using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField] private GameObject _activePage;
    private List<Transform> _pages = new List<Transform>();
    private int _currentPageNmb;
    // Start is called before the first frame update
    void Start()
    {
        _currentPageNmb = _activePage.transform.GetSiblingIndex();
        for (int i = 0; i < transform.childCount; i++) {
            Transform page = transform.GetChild(i);
            _pages.Add(page);
        }
    }

    public void NextPage() {
        _currentPageNmb = Check(_currentPageNmb + 1);
        SetNewPage();
    }
    public void PrevPage() {
        _currentPageNmb = Check(_currentPageNmb - 1);
        SetNewPage();
    }
    private void SetNewPage() {
        _activePage = transform.GetChild(_currentPageNmb).gameObject;
        SwitchMenuItem switchMenuItem = _activePage.GetComponent<SwitchMenuItem>();
        switchMenuItem._switchpages.ChangeActiveItems(switchMenuItem);
    }
    private int Check(int nmb) {
        if (nmb >= _pages.Count) return 0;
        if (nmb < 0) return _pages.Count - 1;
        return nmb;
    }
}
