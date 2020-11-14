using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

interface IUpdateDelete {
    //void DownloadCache();
    void UpdateCache();
    void DeleteCache();
}

public class ActionButton : MonoBehaviour, IUpdateDelete
{
    private InstitutionInfoVariables _institutionInfoVariables;
    private void Start()
    {
        _institutionInfoVariables = GetComponentInParent<InstitutionInfoVariables>();
    }
    public void DeleteCache()
    {
         foreach (GameObject i in OrganizationControl.organization_list)
         {
             if (i.GetComponentInParent<InstitutionInfoVariables>().id == _institutionInfoVariables.id) 
             {
                OrganizationControl.organization_list.Remove(i);
                GameObject obj = OrganizationDownload.CreateDownload(_institutionInfoVariables.gameObject.GetComponent<InformationAboutOrganization>().Institution, OrganizationControl.content);
                //передадим в объект изображение логотипа из текущего объекта
                obj.GetComponent<InformationAboutOrganization>().Image.texture = _institutionInfoVariables.gameObject.GetComponent<InformationAboutOrganization>().Image.texture;

                Directory.Delete(_institutionInfoVariables.cachePath, true);

                Destroy(_institutionInfoVariables.gameObject);
                break;
             }

         }

    }

    public void DownloadCache() {
        GlobalVariables.checkedID = _institutionInfoVariables.id;
        SceneChanger.OpenScenLoader(0);
    }
    public void UpdateCache() {
        DeleteCache();
        SceneChanger.OpenScenLoader(0);
    }
}
