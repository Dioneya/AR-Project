using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using TMPro;

public class InstitutionsManager : MonoBehaviour
{
    public InstitutionJsonLoader.InstitutionList institutionList = new InstitutionJsonLoader.InstitutionList();
    public List<GameObject> organizationsList {get;} = new List<GameObject>();
    [SerializeField] GameObject organization, detailsPage, retry;
    [SerializeField] TextMeshProUGUI scannerText, organizationsText;
    static public UnityEvent PickOrganization;
    void Start()
    {
        StartCoroutine(GetJSONFromServer());
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        PickOrganization = new UnityEvent();
        PickOrganization.AddListener(ShowDetailsPage);
    }

    private void CheckInternetConnection() 
    {
        retry.SetActive(Application.internetReachability == NetworkReachability.NotReachable);
    }

    IEnumerator GetJSONFromServer()
    {
        CheckInternetConnection();
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

    public void ChangeToQR()
    {
        organizationsText.color = scannerText.color;
        scannerText.color = Color.black;
    }
    public void ChangeToOrganization()
    {
        scannerText.color = organizationsText.color;
        organizationsText.color = Color.black;
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