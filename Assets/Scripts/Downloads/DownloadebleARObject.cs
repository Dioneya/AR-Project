using System.Collections;
using UnityEngine;
using Vuforia;
/// <summary>
/// ����� ������������ AR �������
/// </summary>
public class DownloadebleARObject : MonoBehaviour, IDownloadeble
{
    protected ARObjectJsonLoader.ArObject JSON { get; set; } // ����� �������� AR �������
    protected string filePath = ""; // ���� � ����� � ����
    protected int id = 0; // id �������

    /// <summary>
    /// ���������� ��������� ������������ �������
    /// </summary>
    /// <param name="ID">id �������</param>
    /// <param name="json">����� �������� AR �������</param>
    public void SetParam(int ID, ARObjectJsonLoader.ArObject json) 
    {
        JSON = json;
        id = ID;
    }
    /// <summary>
    /// ���������� ���� � AR ������� � ����
    /// </summary>
    protected void FilePathDetect() 
    {
        filePath = PathWorker.CacheFilePath(id, (ArObjects.Type)JSON.object_type.value);
    }
    /// <summary>
    /// ��������� AR ������ �� ������
    /// </summary>
    /// <param name="trackableBehaviour">������ �������</param>
    public virtual void Download(DataSetTrackableBehaviour trackableBehaviour){}
    /// <summary>
    /// ��������� �������� ��� ����������� ������� 2D �������� �� �������
    /// </summary>
    /// <param name="texture">�����������</param>
    /// <returns></returns>
    protected GameObject GenerateQuad(Texture2D texture)
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        quad.GetComponent<Renderer>().material.mainTexture = texture;
        return quad;
    }
    /// <summary>
    /// ������� ������� 
    /// </summary>
    /// <param name="url">����/������ � �������</param>
    /// <returns></returns>
    protected virtual IEnumerator LoadARObject(string url)
    {   
        yield return null;
    } 
}