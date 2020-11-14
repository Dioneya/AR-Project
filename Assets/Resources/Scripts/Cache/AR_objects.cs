using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

public class AR_objects : MonoBehaviour
{
    protected string cacheFilePath;
    protected string folder;
    protected string filename;
    protected string url;
    protected bool isDone = false;
    protected static string GetUrl(int id) {
        var markerInfo = GlobalVariables.institution.data.markers;

        #region Проверка на тип ссылки объекта 
        string url = markerInfo[id].a_r_object.object_path.url;
        if (url != null)
        {
            if (url.Contains("http"))
                return url; //url ссылка на объект
            else
                return GlobalVariables.link + url;
        }
        return "";
        #endregion
    }
    protected static string GetFileName(int id) {
        return GlobalVariables.institution.data.markers[id].a_r_object.id.ToString();
    }
    public IEnumerator StartDownloadAndCache(string cacheFilePath, string folder, string filename, string url) {
        if (!CacheChecker.CheckFile(cacheFilePath))
        {
            StartCoroutine(Cache(folder, filename, url));
            yield return new WaitWhile(() => !isDone);
            isDone = false;
        }
    }

    virtual public IEnumerator Cache(string folder, string fileName, string url) {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        string pathFolder = Path.Combine(PathWorker.cachePathFolder, folder);
        string cacheFilePath = Path.Combine(pathFolder, fileName);
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            File.WriteAllBytes(cacheFilePath, request.downloadHandler.data);
        }
        isDone = true;
    }
}
public class AR_marker : AR_objects, ICacheble
{ 
    public IEnumerator DownloadAndCache(int id)
    {
        var markerInfo = GlobalVariables.institution.data.markers;
        for (int i = 0; i < markerInfo[id].image_set.Count; i++)
        {
            cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Marker, i.ToString());            
            folder = PathWorker.MarkerStrPath(id);
            filename = i.ToString();
            url = GlobalVariables.link + markerInfo[id].image_set[i].url;

            yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
        }
    }
    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        StartCoroutine(base.Cache(folder, fileName + ".png", url));
        yield return null;
    }
}

public class AR_logo : AR_objects, ICacheble
{  
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Logo);       
        folder = Path.Combine("institution_" + GlobalVariables.institution.data.id.ToString());
        filename = "logo";
        url = GlobalVariables.link + GlobalVariables.institution.data.image;
        
        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }

    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        StartCoroutine(base.Cache(folder, fileName + ".png", url));
        yield return null;
    }
}

public class AR_sticker : AR_objects, ICacheble
{   
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Sticker);       
        folder = PathWorker.MarkerStrPath(id);
        filename = "sticker";
        url = GlobalVariables.link + GlobalVariables.institution.data.markers[id].sticker_image;

        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }

    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        StartCoroutine(base.Cache(folder, fileName + ".png", url));
        yield return null;
    }
}

public class AR_image : AR_objects, ICacheble
{    
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Image);       
        folder = PathWorker.ArObjStrPath(id);
        filename = GetFileName(id);
        url = GetUrl(id);

        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }

    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        StartCoroutine(base.Cache(folder, fileName + ".png", url));
        yield return null;
    }
}

public class AR_video : AR_objects, ICacheble
{
    
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Video);       
        folder = PathWorker.ArObjStrPath(id);
        filename = GetFileName(id);
        url = GetUrl(id);

        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }

    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        StartCoroutine(base.Cache(folder, fileName + ".mp4", url));
        yield return null;
    }
}

public class AR_model : AR_objects, ICacheble
{   
    public IEnumerator DownloadAndCache(int id)
    {
        throw new MissingMethodException();
    }

    public IEnumerator Cache(string folder, string fileName, string url)
    {
        throw new NotImplementedException();
    }
}

public class AR_audio : AR_objects, ICacheble
{
    
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Audio);       
        folder = PathWorker.ArObjStrPath(id);
        filename = GetFileName(id);
        url = GetUrl(id);

        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }
    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        StartCoroutine(base.Cache(folder, fileName + ".mp3", url));
        yield return null;
    }
}

public class AR_assetBundle : AR_objects, ICacheble
{  
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.AssetBundle); 
        folder = PathWorker.ArObjStrPath(id);
        filename = GetFileName(id);
        url = GetUrl(id);
        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }
}

public class AR_animatedAssetBundle : AR_objects, ICacheble
{
    public IEnumerator DownloadAndCache(int id)
    {
        cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.AssetBundle);
        folder = PathWorker.ArObjStrPath(id);
        filename = GetFileName(id);
        url = GetUrl(id);

        yield return StartCoroutine(StartDownloadAndCache(cacheFilePath, folder, filename, url));
    }
}

public class AR_text : AR_objects, ICacheble
{
    public IEnumerator DownloadAndCache(int id)
    {
        var cacheFilePath = PathWorker.CacheFilePath(id, AR_Objects.Type.Text);
        var pathFolder = Path.Combine(PathWorker.cachePathFolder, PathWorker.ArObjStrPath(id));

        Debug.Log(cacheFilePath);
        Debug.Log(pathFolder);

        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        File.WriteAllText(cacheFilePath, GlobalVariables.institution.data.markers[id].a_r_object.object_settings.text);
        yield return null;
    }

    override public IEnumerator Cache(string folder, string fileName, string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        string cacheFilePath = Path.Combine(PathWorker.cachePathFolder, folder, fileName + ".txt");
        if (request.isHttpError && request.isNetworkError)
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);
        }
        else
        {
            File.WriteAllText(cacheFilePath, request.downloadHandler.text);
        }
        request.Dispose();
    }

    public static void CacheText(string folder, string fileName, string data)
    {
        string pathFolder = Path.Combine(PathWorker.cachePathFolder, folder);
        string cacheFilePath = Path.Combine(pathFolder, fileName + ".txt");
        if (!Directory.Exists(pathFolder))
        {
            Directory.CreateDirectory(pathFolder);
        }
        Debug.LogWarning(cacheFilePath);

        File.WriteAllText(cacheFilePath, data);
    }
}
