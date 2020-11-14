using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayElements : MonoBehaviour
{
    [SerializeField] private List<GameObject> mustBeShowed = new List<GameObject>();
    [SerializeField] private List<GameObject> mustBeHidden = new List<GameObject>();
    // Start is called before the first frame update

    public void ShowElements() {
        AllElements(mustBeShowed, true);
    }
    public void HideElements()
    {
        AllElements(mustBeHidden, false);
    }

    private void AllElements(List<GameObject> list, bool condition) {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(condition);
        }
    }
    public void ShowAndHideElements()
    {
        ShowElements();
        HideElements();
    }
}
