using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TabsMenu : MonoBehaviour
{
    [SerializeField] private List<TabsItem> tabsItems;
    [SerializeField] private Color32 activeColor;
    [SerializeField] private Color32 unactiveColor;

    [SerializeField] private Image ScrollHandler;

    Coroutine CoroutineAnimation;
    // Start is called before the first frame update
    bool isAnimationPlayed = false;
    void Start()
    {
        DisableAllItems();
        ActivateItem(0);
        AddClickEventOnTabsItems();
    }
    public void DisableAllItems(){
        for (int i = 0; i < tabsItems.Count; i++) {
            tabsItems[i].textMesh.color = unactiveColor;
        }
    }

    public void ActivateItem(int itemNumber) {
        tabsItems[itemNumber].textMesh.color = activeColor;
    }

    private void AddClickEventOnTabsItems() {

        for (int i = 0; i < tabsItems.Count; i++)
        {
            int nmb = i;
            tabsItems[i].button.onClick.AddListener(()=> { 
                DisableAllItems(); 
                ActivateItem(nmb);
                if (isAnimationPlayed == false)
                {

                    CoroutineAnimation = StartCoroutine(MoveHandlerToActiveItem(nmb));
                }
                else
                {
                    StopCoroutine(CoroutineAnimation);
                    ScrollHandler.transform.localPosition = new Vector3(tabsItems[nmb].transform.localPosition.x, ScrollHandler.transform.localPosition.y, ScrollHandler.transform.localPosition.z);
                    isAnimationPlayed = false;
                }
            });
        }
    }

    private IEnumerator MoveHandlerToActiveItem(int itemNumber) {
        Vector3 scrollhandlerPosition = ScrollHandler.transform.localPosition;
        Vector3 destination = new Vector3(tabsItems[itemNumber].transform.localPosition.x, scrollhandlerPosition.y, scrollhandlerPosition.z);
            float totalMovementTime = 1f;
            float currentMovementTime = 0f;
        isAnimationPlayed = true;
        while (Vector3.Distance(ScrollHandler.transform.localPosition, destination) != 0)
            {
                currentMovementTime += Time.deltaTime;
                ScrollHandler.transform.localPosition = Vector3.Lerp(ScrollHandler.transform.localPosition, destination, currentMovementTime / totalMovementTime);
                Debug.Log(ScrollHandler.transform.localPosition);
                yield return null;
            }
        isAnimationPlayed = false;
    }
}
