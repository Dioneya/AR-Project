using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using UnityEngine;
using Vuforia;

public class Marker : MonoBehaviour
{
    private string name = "";
    private MarkerJsonLoader.Marker markerJSON;
    private string imageFilePath = "";
    private Transform ARCamera;

    public GameObject markerObj {get; private set;}
    private ObjectTracker objectTracker;
    public DataSetTrackableBehaviour trackableBehaviour;
    public Marker(string _name, string _imageFilePath, Transform _ARCameraTransform, ObjectTracker _objectTracker) 
    {
        name = _name;
        imageFilePath = _imageFilePath;
        ARCamera = _ARCameraTransform;
        objectTracker = _objectTracker;

        Create();
    }

    private void Create() 
    {
        Texture2D texture = GetTextureFormPath();
        RuntimeImageSource runtimeImageSource = GetRuntimeImgSource(texture, name);

        var dataset = objectTracker.CreateDataSet();
        trackableBehaviour = dataset.CreateTrackable(runtimeImageSource, name);
       
        trackableBehaviour.gameObject.AddComponent<DefaultTrackableEventHandler>();
        trackableBehaviour.gameObject.AddComponent<BoxCollider>();
        
        objectTracker.ActivateDataSet(dataset);


        trackableBehaviour.transform.SetParent(ARCamera);

        markerObj = trackableBehaviour.gameObject;
    }

    private Texture2D GetTextureFormPath() 
    {
        var texture = new Texture2D(4, 4);
        texture.LoadImage(File.ReadAllBytes(imageFilePath));
        return texture;
    }

    private RuntimeImageSource GetRuntimeImgSource(Texture2D texture, string _name) 
    {
        var runtimeImageSource = objectTracker.RuntimeImageSource;
        runtimeImageSource.SetImage(texture, 1.5f, name);

        return runtimeImageSource;
    }
}
