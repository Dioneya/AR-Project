using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class AssetDownload : DownloadebleARObject
{
    private ARObjectJsonLoader.Transform transform_obj;
    private string nameObj="";

    private void Initialize()
    {
        transform_obj = JSON.transform;
        nameObj = JSON.object_path.name;
    }

    public override void Download(DataSetTrackableBehaviour trackableBehaviour)
    {
        FilePathDetect();
        Initialize();
        string url =  "file:///" + filePath;
        transform.SetParent(trackableBehaviour.gameObject.transform);
        StartCoroutine(LoadARObject(url));
    }

    protected override IEnumerator LoadARObject(string url)
    {
        var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return request.SendWebRequest();

        AssetBundle bundle = (DownloadHandlerAssetBundle.GetContent(request));

        if (request.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset(nameObj);

            #region Позиционированние объекта

            bool isNullScale =  transform_obj.scale.x == 0 || transform_obj.scale.y == 0 || transform_obj.scale.z == 0;
            Vector3 defaultScale = new Vector3(0.3f, 0.3f, 0.3f);
            Vector3 objScale = new Vector3(transform_obj.scale.x, transform_obj.scale.y, transform_obj.scale.z);
            Vector3 objPos = new Vector3(transform_obj.position.x, transform_obj.position.y, transform_obj.position.z);

            GameObject asset = Instantiate(obj, transform);

            if(transform_obj != null)
            {
                asset.transform.localPosition = objPos;
                asset.transform.localScale = isNullScale ? defaultScale : objScale;   
            }
                
            #endregion


            /*#region Создание и настройка ActionLink на объекте
            var action = asset.AddComponent<ActionLink>();
            action.action_link = action_link;
            #endregion*/

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
