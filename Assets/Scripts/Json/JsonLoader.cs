using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

/// <summary>
/// Класс обработки JSON файла полученный с сервера 
/// </summary>
public class JsonLoader : MonoBehaviour
{
    private CacheChecker cacheChecker;
    private ServerDownloader serverDownloader;
    //SceneLoader sceneLoader;

    public InstitutionJsonLoader.Institution institution;
    public string jsonUrl;
    public string id;
    private bool mustCache = true;

    void Awake()
    {
        cacheChecker = GetComponent<CacheChecker>();
        serverDownloader = GetComponent<ServerDownloader>();
    }
    void Start()
    {
        //sceneLoader = GetComponent<SceneLoader>();
        StartLoad();
    }

    /// <summary>
    /// Начать загрузку JSON файла по глобальному ID текущего заведения
    /// </summary>
    public void StartLoad()
    {
        //var cacheText = cacheChecker.GetTextFromCache("json",Convert.ToString(qrScn.checkedID));
        var cacheText = cacheChecker.GetTextFromCache("json", "institution_" + GlobalVariables.checkedID);
        if (cacheText != null)
        {
            Debug.LogWarning("Кэш найден");
            ProcessJson(cacheText);
        }
        else
        {
            Debug.LogWarning("Кэш не найден, выполнена загрузка");
            jsonUrl = GlobalVariables.link + "/api/institution/" + GlobalVariables.checkedID;
            StartCoroutine(LoadJson());
        }
    }
    /// <summary>
    /// Загрузка с сервера JSON файла
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadJson()
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
        DownloadHandler downloadHandler = www.downloadHandler;
        yield return www.SendWebRequest();
        if (www.error == null)
        {
            ProcessJson(downloadHandler.text);
            Debug.Log(downloadHandler.text);
        }
        else
        {
            Debug.Log("invalid url");
            StartCoroutine(LoadJson());
        }
    }

    /// <summary>
    /// Сериализация полученного JSON файла в класс хранящийся в глобальной переменной
    /// </summary>
    /// <param name="jsonText">JSON текст</param>
    private void ProcessJson(string jsonText)
    {
        GlobalVariables.institution = JsonUtility.FromJson<InstitutionJsonLoader.Institution>(jsonText);
        institution = GlobalVariables.institution;
        Debug.LogWarning(GlobalVariables.institution.data.title);
        if (mustCache)
        {
            AR_text.CacheText("institution_" + Convert.ToString(GlobalVariables.institution.data.id), "json", jsonText);
        }

        StartCoroutine(serverDownloader.Download(GlobalVariables.institution));
        //sceneLoader.LoadScene(sceneLoader.SceneNmb);
    }
}
