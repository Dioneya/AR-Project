using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class InstitutionsManager : MonoBehaviour
{
    public InstitutionJsonLoader.InstitutionList institutionList = new InstitutionJsonLoader.InstitutionList();
    private List<GameObject> organizationsList = new List<GameObject>();
    [SerializeField] GameObject organization, detailsPage;
    static public UnityEvent PickOrganization;
    void Start()
    {
        StartCoroutine(GetJSONFromServer());
    }

    private void OnEnable()
    {
        PickOrganization = new UnityEvent();
        PickOrganization.AddListener(ShowDetailsPage);
    }

    IEnumerator GetJSONFromServer()
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link+"/public/api/institution");
        DownloadHandler downloadHandler = www.downloadHandler;
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            institutionList = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(downloadHandler.text);
            Debug.Log(downloadHandler.text);
            ShowOrganizations();
        }
        else {
            Debug.Log("invalid url");
        }
    }

    public void Restart() 
    {
        ClearList();
        StartCoroutine(GetJSONFromServer());
    }

    private void ClearList() 
    {
        foreach (GameObject organ in organizationsList) 
        {
            Destroy(organ);
        }
        organizationsList.Clear();
    }

    private void ShowDetailsPage() 
    {
        detailsPage.SetActive(true);
    }

    void ShowOrganizations() 
    {
        foreach (InstitutionJsonLoader.InstitutionObj institution in institutionList.data) 
        {
            GameObject org = Instantiate(organization, transform);
            org.GetComponent<Organization>().SetParam(institution);
            organizationsList.Add(org);
        }
    }
}