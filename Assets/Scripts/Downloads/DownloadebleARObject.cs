using System.Collections;
using UnityEngine;
using Vuforia;
/// <summary>
/// Класс загружаемого AR объекта
/// </summary>
public class DownloadebleARObject : MonoBehaviour, IDownloadeble
{
    protected ARObjectJsonLoader.ArObject JSON { get; set; } // Класс описания AR объекта
    protected string filePath = ""; // путь к файлу в кэше
    protected int id = 0; // id маркера

    /// <summary>
    /// Установить параметры загружаемого объекта
    /// </summary>
    /// <param name="ID">id маркера</param>
    /// <param name="json">Класс описания AR объекта</param>
    public void SetParam(int ID, ARObjectJsonLoader.ArObject json) 
    {
        JSON = json;
        id = ID;
    }
    /// <summary>
    /// Нахождения пути к AR объекту в кэше
    /// </summary>
    protected void FilePathDetect() 
    {
        filePath = PathWorker.CacheFilePath(id, (ArObjects.Type)JSON.object_type.value);
    }
    /// <summary>
    /// Загрузить AR объект на маркер
    /// </summary>
    /// <param name="trackableBehaviour">объект маркера</param>
    public virtual void Download(DataSetTrackableBehaviour trackableBehaviour){}
    /// <summary>
    /// Генерация квадрата для отображения плоских 2D объектов на маркере
    /// </summary>
    /// <param name="texture">Изображение</param>
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
    /// Выкачка объекта 
    /// </summary>
    /// <param name="url">Путь/адресс к объекту</param>
    /// <returns></returns>
    protected virtual IEnumerator LoadARObject(string url)
    {   
        yield return null;
    } 
}