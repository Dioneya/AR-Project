using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
public class PathWorker
{
    public static string cachePathFolder;

    private static Dictionary<ArObjects.Type, Delegate> paths = new Dictionary<ArObjects.Type, Delegate>()
    {
        {ArObjects.Type.Image, new Func<int, string>(ArObjStrPath)},
        {ArObjects.Type.Logo,  new Func<int, string>(InstitutionPath)},
        {ArObjects.Type.Sticker, new Func<int, string>(MarkerStrPath)},
        {ArObjects.Type.Marker, new Func<int, string>(MarkerStrPath)},
        {ArObjects.Type.AssetBundle, new Func<int, string>(ArObjStrPath)},
        {ArObjects.Type.Audio, new Func<int, string>(ArObjStrPath)},
        {ArObjects.Type.Video, new Func<int, string>(ArObjStrPath)},
        {ArObjects.Type.Text, new Func<int, string>(ArObjStrPath)},
        {ArObjects.Type.AnimatedAssetBundle, new Func<int, string>(ArObjStrPath)}
    };
    private static Dictionary<ArObjects.Type, string> formats = new Dictionary<ArObjects.Type, string>()
    {
        {ArObjects.Type.Image, ".png"},
        {ArObjects.Type.Logo,  "logo.png"},
        {ArObjects.Type.Sticker, "sticker.png"},
        {ArObjects.Type.Marker, ".png"},
        {ArObjects.Type.AssetBundle, ""},
        {ArObjects.Type.Audio, ".mp3"},
        {ArObjects.Type.Video, ".mp4"},
        {ArObjects.Type.Text, ".txt"},
        {ArObjects.Type.AnimatedAssetBundle, ""}
    };

    [RuntimeInitializeOnLoadMethod]
    public static void Start() {
        cachePathFolder = InitCachePathFolder();
    }

    private static string InitCachePathFolder() { 
        return Path.Combine(Application.persistentDataPath, @"Cache");
    }

    public static string MarkerStrPath(int indexMarker)//возвращает путь к папке с маркером 
    {
        InstitutionJsonLoader.InstitutionObj institutionData = GlobalVariables.institution.data;
        MarkerJsonLoader.Marker institutionMarker = institutionData.markers[indexMarker];

        return MarkerPath(institutionData, institutionMarker);
    }

    public static string ArObjStrPath(int indexMarker) // возвращает путь к папке кэша объекта
    {
        InstitutionJsonLoader.InstitutionObj institutionData = GlobalVariables.institution.data;
        MarkerJsonLoader.Marker institutionMarker = institutionData.markers[indexMarker];

        return Path.Combine(MarkerPath(institutionData, institutionMarker), "arObject_" + institutionMarker.a_r_object.id);
    }
    private static string MarkerPath(InstitutionJsonLoader.InstitutionObj institutionData, MarkerJsonLoader.Marker institutionMarker) {
        return Path.Combine(InstitutionPath(institutionData.id), "marker_" + institutionMarker.id);
    }
    public static string InstitutionPath(int id) {
        return Path.Combine(cachePathFolder, "institution_" + id);
    }
    public static string CacheFilePath(int indexOfMarker, ArObjects.Type type, string fileName = "") //возвращает путь к файлу кэша объекта
    {
        if (fileName == String.Empty && GlobalVariables.institution.data.markers.Count!=0)
            fileName = GlobalVariables.institution.data.markers[indexOfMarker].a_r_object.id.ToString();
        
        var method = paths[type].DynamicInvoke(type == ArObjects.Type.Logo ? GlobalVariables.institution.data.id : indexOfMarker);
        string cacheFilePath = Path.Combine((string)method, fileName+formats[type]);
        
        return cacheFilePath;
    }
}