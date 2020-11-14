using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InformationAboutOrganization : MonoBehaviour
{
    public RawImage Image;
    public TextMeshProUGUI OrganizationName;
    public TextMeshProUGUI OrganizationMarkersCount;
    public TextMeshProUGUI OrganizationAddress;
    public InstitutionJsonLoader.InstitutionList Institution;
    public bool isAviable = false;
    private void Start()
    {
        Button button = transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(ButtonProcess);
    }

    void ButtonProcess()
    {
        if (isAviable) 
        {
            //Cмена кэша
            if (GlobalVariables.institution == null || GlobalVariables.institution.data.id != Institution.data.id)
            {
                GlobalVariables.institution = Institution;
                GlobalVariables.isInstChanged = true;
                Debug.Log("Заведение заменено");
            }
            else //открытия меню стикеров
            {
                MarkerInfo markerInfo = transform.parent.gameObject.GetComponent<MarkerInfo>();
                markerInfo.ScrollView.SetActive(true);
                markerInfo.ContentSticker.GetComponent<StickerImage>().StartLoad(Institution);
                markerInfo.Organizations.SetActive(false);
            }
        }
        
    }
}
