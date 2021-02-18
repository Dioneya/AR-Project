using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class VideoDownload : DownloadebleARObject, IPlayable
{
    public VideoPlayer video;
    public string videoURL;

    public void InitVideoPlayer(string filePath) 
    {
        GameObject quad = GenerateQuad(null);

        #region Добавление и настройка VideoPlayer
        video = quad.AddComponent<VideoPlayer>();
        video.playOnAwake = false;
        video.renderMode = VideoRenderMode.MaterialOverride;
        #endregion

        video.url = filePath;
    }

    public override void Download(DataSetTrackableBehaviour trackableBehaviour)
    {
        trackableBehaviour.gameObject.AddComponent<VideoTracker>();

        FilePathDetect();
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