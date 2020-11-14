using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject SliderLoad;
    [SerializeField] private Slider Slider;
    [SerializeField] private TextMeshProUGUI LoadingObjectText;

    public int SceneNmb;
    private static int sceneNmb;
    private float progress = 0;
    private bool isLoadedContent = false;
    private bool sceneLoad = false;
    public static string link = GlobalVariables.link;

    private Dictionary<int, ICacheble> _arObjects = new Dictionary<int, ICacheble>();
    private Dictionary<int, ICacheble> _arMarkers = new Dictionary<int, ICacheble>();
    private Dictionary<int, ICacheble> _arStickers = new Dictionary<int, ICacheble>();
    private Dictionary<int, ICacheble> _arLogo = new Dictionary<int, ICacheble>();
    private float progress_cnt;
    void Start()
    {
        SceneNmb = sceneNmb;
        //LoadScene(SceneNmb);
    }
    public static void SetSceneNmb(int snmb) {
        sceneNmb = snmb;
    }
    public void LoadScene(int sceneIndex) {

        StartCoroutine(LoadAsunc(sceneIndex));
    }
    IEnumerator LoadAsunc(int sceneIndex) {

        SliderLoad.SetActive(true);
        LoadingObjectText.gameObject.SetActive(true);

        LoadContent(); //запуск метода по закачке данных + кеширование

        //SceneManager.LoadSceneAsync(sceneIndex); //переход на другую сцену

        yield return null;
    }

    private void Update()
    {
        #region Загрузочная шкала 
        Slider.value = Mathf.Lerp(Slider.value, progress / 100, 0.5f);
        LoadingObjectText.text = string.Format(($"{progress:0}%"));
        #endregion

        if (Slider.value == 1)
        {
            LoadingScreenAnimation.isPlayAnimation = false;
            isLoadedContent = true;
        }
        if (isLoadedContent == true && sceneLoad == false) { 
            SceneManager.LoadSceneAsync(sceneNmb);
            sceneLoad = true;
        }
    }

    void LoadContent()
    {
        GlobalVariables.isInstChanged = true;
        var markerInfo = GlobalVariables.institution.data.markers;
        ICacheble cacheble;

       /* if (markerInfo.Count == 0) 
        {
        }*/
            _arLogo.Add(GlobalVariables.institution.data.id, gameObject.AddComponent<AR_logo>());

           // _arLogo.Add(i, gameObject.AddComponent<AR_logo>());
        for (int i = 0; i < markerInfo.Count; i++)
        {

            int key = markerInfo[i].a_r_object.object_type.value; //переменная которая указывает на id типа объекта
                                                                  //string arObjectID = markerInfo[i].a_r_object.id.ToString();
            _arMarkers.Add(i, gameObject.AddComponent<AR_marker>());
            _arStickers.Add(i, gameObject.AddComponent<AR_sticker>());

            switch (AR_Objects.ARObjectsList[key])
            {
                case AR_Objects.Type.Image:
                {
                    cacheble = gameObject.AddComponent<AR_image>();
                    break;
                }

                case AR_Objects.Type.Video:
                {
                    cacheble = gameObject.AddComponent<AR_video>();
                    break;
                }

                case AR_Objects.Type.Model:
                {
                    cacheble = gameObject.AddComponent<AR_model>();
                    break;
                }

                case AR_Objects.Type.Audio:
                {
                    cacheble = gameObject.AddComponent<AR_audio>();
                    break;
                }

                case AR_Objects.Type.AssetBundle:
                {
                    cacheble = gameObject.AddComponent<AR_assetBundle>();
                    break;
                }

                case AR_Objects.Type.AnimatedAssetBundle:
                {
                    cacheble = gameObject.AddComponent<AR_animatedAssetBundle>();
                    break;
                }

                case AR_Objects.Type.Text:
                {
                    cacheble = gameObject.AddComponent<AR_text>();
                    break;
                }
                default: { 
                    throw new ArgumentNullException();
                }

            }
            AddToDownloadingObjects(i, cacheble);
            
            Debug.LogWarning(GlobalVariables.link + markerInfo[i].image_set[0].url);
        }
        StartCoroutine(StartDownloadFromDictionaries());
    }
    private void AddToDownloadingObjects(int id, ICacheble cacheble) {
        _arObjects.Add(id, cacheble);
    }

    private IEnumerator StartDownloadFromDictionaries()
    {
        progress_cnt = 100f / (_arLogo.Count + _arMarkers.Count + _arObjects.Count + _arStickers.Count);
        yield return StartCoroutine(DownloadAllElements(_arLogo));
        yield return StartCoroutine(DownloadAllElements(_arStickers));
        yield return StartCoroutine(DownloadAllElements(_arMarkers));
        yield return StartCoroutine(DownloadAllElements(_arObjects));
        progress = (float)Math.Round(progress);
    }
    private IEnumerator DownloadAllElements(Dictionary<int, ICacheble> dictionary) {
        foreach (KeyValuePair<int, ICacheble> keyValue in dictionary)
        {
            yield return StartCoroutine(dictionary[keyValue.Key].DownloadAndCache(keyValue.Key));
            progress += progress_cnt;
        }
    }
}
