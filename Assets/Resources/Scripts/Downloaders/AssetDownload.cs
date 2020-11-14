using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
public class AssetDownload : MonoBehaviour, IDownloadeble
{
    #region Публичные переменные, которые хранят параметры объекта
    private string url;
    private string nameObj="";
    //public bool isTest = false;

    private string action_link;
    private ARObjectJsonLoader.Transform transform_obj;
    #endregion
   /* void Start()
    {
        if (isTest) // Если это для теста, то сразу запустит метод загрузки объекта
        {
            StartLoad();
        }
    }*/

    public void StartDownload(string filePath, DataSetTrackableBehaviour trackableBehaviour)
    {
        #region Заполнение информации о 3D модели

        url = "file:///" + filePath;
        transform.SetParent(trackableBehaviour.gameObject.transform);
        #endregion
        StartCoroutine(LoadAssetURL());
    }

    public void SetParam(string name, ARObjectJsonLoader.Transform _transform, string _action_link = "")
    {
        nameObj = name;
        transform_obj = _transform;
        action_link = _action_link;
    }

    IEnumerator LoadAssetURL() 
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return request.SendWebRequest();

        AssetBundle bundle = (DownloadHandlerAssetBundle.GetContent(request));

        if (request.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset(nameObj);

            #region Позиционированние объекта
            if (transform_obj == null) { }
            else if ( transform_obj.scale.x == 0 || transform_obj.scale.y == 0 || transform_obj.scale.z == 0)
            {
                obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            else 
            {
                obj.transform.localScale = new Vector3(transform_obj.scale.x, transform_obj.scale.y, transform_obj.scale.z);
            }
            if(transform_obj != null)
                obj.transform.localPosition = new Vector3(transform_obj.position.x, transform_obj.position.y, transform_obj.position.z);
            #endregion

            GameObject asset = Instantiate(obj, transform);

            #region Создание и настройка ActionLink на объекте
            var action = asset.AddComponent<ActionLink>();
            action.action_link = action_link;
            #endregion

            #region Создание и настройка AssetTracker
            var tracker = gameObject.AddComponent<AssetTracker>();
            tracker.model = asset;
            #endregion
        }
        else 
        {
            Debug.Log(request.error);
        }
        bundle.Unload(false);
    }
}
