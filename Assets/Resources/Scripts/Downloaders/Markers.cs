using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System;
using System.IO;
using System.Collections;

public class Markers : MonoBehaviour
{
    private List<MarkerJsonLoader.Marker> _markerInfo;
    private List<GameObject> makerObjectsList = new List<GameObject>();
    private ObjectTracker _objectTracker;
    [SerializeField] private Camera _LoadingCamera;

    void Start()
    {
        
        _LoadingCamera.enabled = true;
        ChangeInst();
    }

    public void ChangeInst() 
    {
        if (GlobalVariables.isInstChanged) 
        {
            ClearList();
            Init();
            GlobalVariables.isInstChanged = false;
        }
        StartCoroutine(DisableLoadingCamera());  // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    void ClearList() 
    {
        if (_objectTracker != null) 
        {
            _objectTracker.DestroyAllDataSets(true);
            _objectTracker = null;
        }  
        foreach (GameObject marker in makerObjectsList) 
        {
            if (marker != null) 
            {
                marker.GetComponent<ImageTargetBehaviour>().enabled = false;
                Destroy(marker);
            }
        }
        makerObjectsList.Clear();
    }

    private IEnumerator Waiter() 
    {
        yield return new WaitForSeconds(.10f);
        Init();
    }
    private void Init()
    {
        _objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (_objectTracker != null)
        {
            if (CustomMarker.isCustom) 
            {
                StartCoroutine(CreateCustomMarker());
                StartCoroutine(DisableLoadingCamera());
            }
            else if (GlobalVariables.institution != null)
            {
                _markerInfo = GlobalVariables.institution.data.markers;

                //Пройдёмся по циклу чтобы выгрузить маркеры
                for (int i = 0; i < _markerInfo.Count; i++)
                {
                    Debug.Log(i + " " + _markerInfo[i].id);
                    StartCoroutine(CreateMarkers(_markerInfo[i])); //Выгружаем маркер по id
                }
                StartCoroutine(DisableLoadingCamera());
            }
            
        }
        else {
            StartCoroutine(Waiter());
        }
    }
    private IEnumerator DisableLoadingCamera() {
        yield return new WaitForSeconds(.15f);
        _LoadingCamera.gameObject.SetActive(false);
    }
    private IEnumerator CreateMarkers(MarkerJsonLoader.Marker _marker)
    {
        for (int i = 0; i < _marker.image_set.Count; i++) 
        {
            string name = _marker.id+"_"+i; // Имя маркера будет являть id 

            var cacheFilePath = PathWorker.CacheFilePath(_marker.id-1, AR_Objects.Type.Marker, i.ToString());
            Debug.Log(cacheFilePath);

            Marker marker = new Marker(name, cacheFilePath, transform, _objectTracker);
            
            ChoseDownloaderScript.SelectDownload("",_marker.a_r_object.object_type.value, ref marker.trackableBehaviour, _marker.action_link, _marker); //Запустим скрипт по выбору сценария загрузки объекта для триггера
            yield return new WaitWhile(() => !ChoseDownloaderScript.isDone);
            ChoseDownloaderScript.isDone = false;
            makerObjectsList.Add(marker.GetMarkerGameObject());
        }
    }

    private IEnumerator CreateCustomMarker() 
    {
        GlobalVariables.institution = null;
        string name = "Custome";
        Marker marker = new Marker(name, CustomMarker.markerPath, transform, _objectTracker);

        ChoseDownloaderScript.SelectDownload(CustomMarker.objectPath, CustomMarker.GetObjectType(), ref marker.trackableBehaviour); //Запустим скрипт по выбору сценария загрузки объекта для триггера
        yield return new WaitWhile(() => !ChoseDownloaderScript.isDone);
        ChoseDownloaderScript.isDone = false;
        makerObjectsList.Add(marker.GetMarkerGameObject());
        CustomMarker.isCustom = false;
    }
}
