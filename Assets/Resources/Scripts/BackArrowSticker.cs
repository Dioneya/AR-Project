using UnityEngine;

public class BackArrowSticker : MonoBehaviour
{
    [SerializeField] private GameObject ScrollView;
    [SerializeField] private GameObject Organizations;
    [SerializeField] private GameObject ContentSticker;
    [SerializeField] private GameObject LocalMap;

    public void Back() 
    {
        if (LocalMap.activeSelf) {
            ScrollView.SetActive(true);
            LocalMap.SetActive(false);
            ContentSticker.transform.parent.gameObject.SetActive(true);
        } 
        else if (ScrollView.activeSelf) {
            Organizations.SetActive(true);
            ScrollView.SetActive(false);
        } 
        if (Organizations.activeSelf) {
            GlobalVariables.Images.Clear();
            for (int i = 0; i < ContentSticker.transform.childCount; i++)
            {
                GameObject sticker = ContentSticker.transform.GetChild(i).gameObject;
                Destroy(sticker);
            }
        }


    }
}
