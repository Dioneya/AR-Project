using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;
public class Search : MonoBehaviour
{
    public GameObject content;
    private void Start()
    {
        StartCoroutine(GetDictionaryOfSearch());
    }
    IEnumerator GetDictionaryOfSearch()
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/institution/dictionary");
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = "{\"list\":" + www.downloadHandler.text + "}";
            GlobalVariables.institutionListInfo = JsonUtility.FromJson<DictionaryOfInstClass.InstitutionList>(json);
            if (GlobalVariables.institutionListInfo == null)
            {
                Debug.Log("Получить данные не удалось!");
            }
            else
            {
                Debug.Log("Данные пользователя получены!");
                DownloadFromDict();
            }
        }
    }

    public void DownloadFromDict() 
    {
        foreach (DictionaryOfInstClass.InstitutionInfo i in GlobalVariables.institutionListInfo.list) 
        {
            StartCoroutine(LoadJson(GlobalVariables.link + "/api/institution/" + i.id));
        }
    }

    public void SearchOrg() 
    {
        string searchWord = GetComponent<TMP_InputField>().text;
        foreach (GameObject i in OrganizationControl.organization_list)
        {
            i.SetActive(i.GetComponent<InstitutionInfoVariables>().nameInst.Contains(searchWord));
        }
    }

    IEnumerator LoadJson(string jsonUrl)
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
        DownloadHandler downloadHandler = www.downloadHandler;
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            InstitutionJsonLoader.InstitutionList inst = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(downloadHandler.text);
            GameObject org = OrganizationDownload.CreateDownload(inst,content);
            if (org!=null) 
            {
                InformationAboutOrganization organizationInformation = org.GetComponent<InformationAboutOrganization>();
                StartCoroutine(ImgDownload.DownloadImage(GlobalVariables.link + inst.data.image, organizationInformation.Image));
            }
            Debug.Log(downloadHandler.text);
        }
        else
        {
            Debug.Log("invalid url");
        }
    }
}
