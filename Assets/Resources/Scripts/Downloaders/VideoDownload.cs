using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class VideoDownload : MonoBehaviour, IDownloadeble, IPlayable
{
    public VideoPlayer video;
    public string videoURL;

    public void InitVideoPlayer(string filePath) 
    {
        #region Создание и настройка места для видео
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        #endregion

        #region Добавление и настройка VideoPlayer
        video = quad.AddComponent<VideoPlayer>();
        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.MaterialOverride;
        #endregion

        video.url = filePath;
    }

    public void StartDownload(string filePath, DataSetTrackableBehaviour trackableBehaviour)
    {
        trackableBehaviour.gameObject.AddComponent<VideoTracker>();
        InitVideoPlayer(filePath);
    }

    public void StartPlay() 
    {
        video.Play();
    }
    public void StopPlay() 
    {
        video.Stop();
    }
}