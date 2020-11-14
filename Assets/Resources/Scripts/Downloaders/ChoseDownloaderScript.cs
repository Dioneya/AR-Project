using System.ComponentModel;
using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class AR_Objects
{
    public enum Type {
        Image,
        Video,
        Model,
        Audio,
        AssetBundle,
        Text,
        Marker,
        AnimatedAssetBundle,
        Sticker,
        Logo
    }
    readonly public static Dictionary<int, Type> ARObjectsList= new Dictionary<int, Type>{
        {0, Type.Image},
        {1, Type.Video},
        {2, Type.Model},
        {3, Type.Audio},
        {4, Type.AssetBundle},
        {5, Type.Text},
        {6, Type.Marker},
        {7, Type.AnimatedAssetBundle},
        {8, Type.Sticker},
        {9, Type.Logo},
    };
}

public class ChoseDownloaderScript : MonoBehaviour
{
    private static  IDownloadeble downloadeble;
    public static bool isDone = false;
    public static void SelectDownload(string filePath, int key, ref DataSetTrackableBehaviour trackableBehaviour, string action_link = "", MarkerJsonLoader.Marker marker = null)
    {
        GameObject trackableBehaviour_GameObject = trackableBehaviour.gameObject;
        if (action_link!="")
            trackableBehaviour_GameObject.AddComponent<ActionLink>().action_link = action_link;

        switch (AR_Objects.ARObjectsList[key]) {
            case AR_Objects.Type.Image: {
                if(marker != null)
                        filePath = PathWorker.CacheFilePath(marker.id-1, AR_Objects.Type.Image);
                downloadeble = trackableBehaviour_GameObject.AddComponent<ImgDownload>();
                break; 
            }
            case AR_Objects.Type.Video: {
                if (marker != null)
                        filePath = PathWorker.CacheFilePath(marker.id-1, AR_Objects.Type.Video);
                downloadeble = trackableBehaviour_GameObject.AddComponent<VideoDownload>();
                break; 
            }
            case AR_Objects.Type.Model: {
                break;
            }
            case AR_Objects.Type.Audio:
            {
                if (marker != null)
                        filePath = PathWorker.CacheFilePath(marker.id-1, AR_Objects.Type.Audio);
                downloadeble = trackableBehaviour_GameObject.AddComponent<AudioDownload>();
                break;
            }
            case AR_Objects.Type.AssetBundle:
            {
                AssetDownload asset = trackableBehaviour_GameObject.AddComponent<AssetDownload>();
                if (marker != null)
                {
                    filePath = PathWorker.CacheFilePath(marker.id-1, AR_Objects.Type.AssetBundle);
                    asset.SetParam(marker.a_r_object.object_path.name, marker.a_r_object.transform, marker.action_link);
                }
                else
                {
                    asset.SetParam(System.IO.Path.GetFileName(filePath), null, action_link);
                }
                downloadeble = asset;
                
                break;
            }
            case AR_Objects.Type.Text:
            {
                TextDownload text = trackableBehaviour_GameObject.AddComponent<TextDownload>();
                if (marker != null)
                        text.text = marker.a_r_object.object_settings.text;
                downloadeble = text;
                break;
            }
            case AR_Objects.Type.AnimatedAssetBundle:
            {
                    AnimatedAssetBundle asset = trackableBehaviour_GameObject.AddComponent<AnimatedAssetBundle>();
                if (marker != null)
                {
                    filePath = PathWorker.CacheFilePath(marker.id-1, AR_Objects.Type.AssetBundle);
                    asset.SetParam(marker.a_r_object.object_path.name, marker.a_r_object.transform, marker.action_link);
                }
                else
                {
                    asset.SetParam(System.IO.Path.GetFileName(filePath), new ARObjectJsonLoader.Transform(), action_link);
                 }

                downloadeble = asset;
                break;
            }
        }
        downloadeble.StartDownload(filePath, trackableBehaviour);
        isDone = true;
    }
}
