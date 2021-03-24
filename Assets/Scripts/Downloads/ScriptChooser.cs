using UnityEngine;
using Vuforia;
using System.Collections.Generic;

/// <summary>
/// Класс который занимается выбором типа загрузки его AR объекта
/// </summary>
public class ScriptChooser : MonoBehaviour
{
    private static IDownloadeble downloadeble; // текущий загружаемый объект
    public static bool isDone = false; // флаг окончания загрузки объекта

    //Словарь типов классов для генерации AR объектов
    private static Dictionary<ArObjects.Type, DownloadebleARObject> dict = new Dictionary<ArObjects.Type, DownloadebleARObject>() 
    {
        {ArObjects.Type.Image, new ImgDownload()},
        {ArObjects.Type.Video, new VideoDownload()},
        {ArObjects.Type.Model, null},
        {ArObjects.Type.Audio, new AudioDownload()},
        {ArObjects.Type.AssetBundle, new AssetDownload()},
        {ArObjects.Type.Text, new TextDownload()},
        {ArObjects.Type.AnimatedAssetBundle, new AssetDownload()}
    };

    /// <summary>
    /// Выбор нужной загрузки AR объекта по заданному типу объекта
    /// </summary>
    /// <param name="key">Тип AR объекта</param>
    /// <param name="trackableBehaviour">DataSet маркера</param>
    /// <param name="marker">Класс описания маркера</param>
    public static void SelectDownload(int key, ref DataSetTrackableBehaviour trackableBehaviour, MarkerJsonLoader.Marker marker)
    {
        GameObject trackableBehaviour_GameObject = trackableBehaviour.gameObject;
        /*if (action_link!="")
            trackableBehaviour_GameObject.AddComponent<ActionLink>().action_link = marker.action_link;*/

        System.Type type = dict[(ArObjects.Type)marker.a_r_object.object_type.value].GetType();
        DownloadebleARObject comp = trackableBehaviour_GameObject.AddComponent(type) as DownloadebleARObject;
        if (marker != null) 
        {
            comp.SetParam(marker.id-1, marker.a_r_object);
            downloadeble = comp;
        }
        downloadeble.Download(trackableBehaviour);
        isDone = true;
    }
}

