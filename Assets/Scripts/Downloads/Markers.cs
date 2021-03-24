using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System.Collections;

/// <summary>
/// Класс отвечающий за маркеры на сцене
/// </summary>
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

    /// <summary>
    /// Замена заведения
    /// </summary>
    public void ChangeInst() 
    {
        if (GlobalVariables.isInstChanged) 
        {
            ClearList();
            Init();
            GlobalVariables.isInstChanged = false;
        }
        StartCoroutine(DisableLoadingCamera());  
    }
    /// <summary>
    /// Удаление маркеров со сцены и очистка списка 
    /// </summary>
    private void ClearList() 
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
    /// <summary>
    /// Ожидание перед повторной попытки Init()
    /// </summary>
    /// <returns></returns>
    private IEnumerator Waiter() 
    {
        yield return new WaitForSeconds(.10f);
        Init();
    }
    /// <summary>
    /// Инициализация маркеров
    /// </summary>
    private void Init()
    {
        _objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        if (_objectTracker != null)
        {
            if (GlobalVariables.institution == null)
                return;

            _markerInfo = GlobalVariables.institution.data.markers;

            //Пройдёмся по циклу чтобы выгрузить маркеры
            for (int i = 0; i < _markerInfo.Count; i++)
            {
                Debug.Log(i + " " + _markerInfo[i].id);
                StartCoroutine(CreateMarkers(_markerInfo[i])); //Выгружаем маркер по id
            }
            StartCoroutine(DisableLoadingCamera());
        }
        else {
            StartCoroutine(Waiter());
        }
    }
    /// <summary>
    /// Отключение загрузочной камеры
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisableLoadingCamera() {
        yield return new WaitForSeconds(.15f);
        _LoadingCamera.gameObject.SetActive(false);
    }
    /// <summary>
    /// Создание маркеров
    /// </summary>
    /// <param name="_marker">Класс данных маркера</param>
    /// <returns></returns>
    private IEnumerator CreateMarkers(MarkerJsonLoader.Marker _marker)
    {
        for (int i = 0; i < _marker.image_set.Count; i++) 
        {
            string name = _marker.id+"_"+i; // Имя маркера будет являть id 

            var cacheFilePath = PathWorker.CacheFilePath(_marker.id-1, ArObjects.Type.Marker, i.ToString());
            Debug.Log(cacheFilePath);

            Marker marker = new Marker(name, cacheFilePath, transform, _objectTracker);
            
            ScriptChooser.SelectDownload(_marker.a_r_object.object_type.value, ref marker.trackableBehaviour, _marker); //Запустим скрипт по выбору сценария загрузки объекта для триггера
            yield return new WaitWhile(() => !ScriptChooser.isDone);
            ScriptChooser.isDone = false;
            makerObjectsList.Add(marker.markerObj);
        }
    }
}
