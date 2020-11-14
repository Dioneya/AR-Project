using UnityEngine;
using System.Collections;
using System.IO;

public class PathWorker
{
    public static string cachePathFolder;

    [RuntimeInitializeOnLoadMethod]
    public static void Start() {
        cachePathFolder = InitCachePathFolder();
    }

    private static string InitCachePathFolder() { 
        return Path.Combine(Application.persistentDataPath, @"Cache");
    }

    public static string MarkerStrPath(int indexMarker)//возвращает путь к папке с маркером 
    {
        InstitutionJsonLoader.Institution institutionData = GlobalVariables.institution.data;
        MarkerJsonLoader.Marker institutionMarker = institutionData.markers[indexMarker];

        return MarkerPath(institutionData, institutionMarker);
    }

    public static string ArObjStrPath(int indexMarker) // возвращает путь к папке кэша объекта
    {
        InstitutionJsonLoader.Institution institutionData = GlobalVariables.institution.data;
        MarkerJsonLoader.Marker institutionMarker = institutionData.markers[indexMarker];

        return Path.Combine(MarkerPath(institutionData, institutionMarker), "arObject_" + institutionMarker.a_r_object.id);
    }
    private static string MarkerPath(InstitutionJsonLoader.Institution institutionData, MarkerJsonLoader.Marker institutionMarker) {
        return Path.Combine(InstitutionPath(institutionData.id), "marker_" + institutionMarker.id);
    }
    public static string InstitutionPath(int id) {
        return Path.Combine(cachePathFolder, "institution_" + id);
    }
    public static string CacheFilePath(int indexOfMarker, AR_Objects.Type type, string fileName = "") //возвращает путь к файлу кэша объекта
    {
        if (fileName == "")
        {
            if (GlobalVariables.institution.data.markers.Count!=0) 
            {
                fileName = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.id.ToString();
            }
        }
        string cacheFilePath = "";
        switch (type)
        {
            case AR_Objects.Type.Logo:
                {
                    cacheFilePath = Path.Combine(InstitutionPath(GlobalVariables.institution.data.id), "logo.png");
                    break;
                }
            case AR_Objects.Type.Sticker:
                {
                    cacheFilePath = Path.Combine(MarkerStrPath(indexOfMarker), "sticker" + ".png");
                    break;
                }
            case AR_Objects.Type.Marker:
                {
                    cacheFilePath = Path.Combine(MarkerStrPath(indexOfMarker), fileName + ".png");
                    break;
                }
            case AR_Objects.Type.Video:
                {
                    cacheFilePath = Path.Combine(ArObjStrPath(indexOfMarker), fileName + ".mp4");
                    break;
                }
            case AR_Objects.Type.Image:
                {
                    cacheFilePath = Path.Combine(ArObjStrPath(indexOfMarker), fileName + ".png");
                    break;
                }
            case AR_Objects.Type.Audio:
                {
                    cacheFilePath = Path.Combine(ArObjStrPath(indexOfMarker), fileName + ".mp3");
                    break;
                }
            case AR_Objects.Type.AssetBundle:
                {
                    cacheFilePath = Path.Combine(ArObjStrPath(indexOfMarker), fileName);
                    break;
                }
            case AR_Objects.Type.Text:
                {
                    cacheFilePath = Path.Combine(ArObjStrPath(indexOfMarker), fileName + ".txt");
                    break;
                }
            default: { break; }
        }

        return cacheFilePath;
    }
}
