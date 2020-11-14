using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
public class MarkerInfo : MonoBehaviour
{
    public GameObject ScrollView;
    public GameObject ContentSticker;
    public GameObject Organizations;
    void Start()
    {
        ScrollView.SetActive(false);
    }

    public void OrgDownload() 
    {
        CacheCheck();
    }

    private void CacheCheck() 
    {
        
        string path = PathWorker.cachePathFolder; //укажем путь к кэшу
        string[] allfolders = Directory.GetDirectories(path); // найдём все папки заведений 

        for (int i = 0; i < allfolders.Length; i++) //обрабатываем все json с элементами
        {
            Debug.LogWarning(allfolders[i]);
            if (File.Exists(Path.Combine(allfolders[i], "json.txt")))
            {
                string jsonPath = Path.Combine(allfolders[i], "json.txt");
                Debug.LogWarning(jsonPath);
                string jsonText = File.ReadAllText(jsonPath);
                var institution = JsonUtility.FromJson<InstitutionJsonLoader.InstitutionList>(jsonText);

                OrganizationDownload.CreateFormDelete(institution, OrganizationControl.content);
            }
        }

    }

    
}
