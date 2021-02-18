using UnityEngine;
using System.IO;
using Vuforia;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
public class ImgDownload : DownloadebleARObject
{
    //public string url;
    public void InitImage(string filePath)
    {

        Texture2D texture = new Texture2D(4, 4);
        texture.LoadImage(File.ReadAllBytes(filePath));

        GameObject quad = GenerateQuad(texture);
        
        quad.GetComponent<Renderer>().material.mainTexture = texture;
    }

    public override void Download(DataSetTrackableBehaviour trackableBehaviour)
    {
        //var obj_path = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.object_path;
        //url = GlobalVariables.link + obj_path.url;
        FilePathDetect();
        InitImage(filePath);
    }
    public static IEnumerator DownloadImage(string MediaUrl, RawImage rawImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }
}
