using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARObject : MonoBehaviour
{
    private DataSetTrackableBehaviour trackableBehaviour;
    public ARObject(DataSetTrackableBehaviour _trackableBehaviour) 
    {

    }

    private IEnumerator Create()
    {
        //ChoseDownloaderScript.SelectDownload(_marker.id, ref trackableBehaviour); //Запустим скрипт по выбору сценария загрузки объекта для триггера
        yield return new WaitWhile(() => !ChoseDownloaderScript.isDone);
        ChoseDownloaderScript.isDone = false;
    }
}
