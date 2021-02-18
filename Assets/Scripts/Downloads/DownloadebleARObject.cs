using System.Collections;
using UnityEngine;
using Vuforia;
public class DownloadebleARObject : MonoBehaviour, IDownloadeble
{
    protected ARObjectJsonLoader.ArObject JSON { get; set; }
    protected string filePath = "";
    protected int id = 0;

    public void SetParam(int ID, ARObjectJsonLoader.ArObject json) 
    {
        JSON = json;
        id = ID;
    }

    protected void FilePathDetect() 
    {
        filePath = PathWorker.CacheFilePath(id, (ArObjects.Type)JSON.object_type.value);
    }

    public virtual void Download(DataSetTrackableBehaviour trackableBehaviour){}

    protected GameObject GenerateQuad(Texture2D texture)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        quad.GetComponent<Renderer>().material.mainTexture = texture;
        return quad;
    }
    protected virtual IEnumerator LoadARObject(string url)
    {   
        yield return null;
    } 
}