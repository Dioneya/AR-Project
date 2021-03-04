using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class AudioDownload : DownloadebleARObject, IPlayable
{
    AudioSource audioSource;
    public override void Download(DataSetTrackableBehaviour trackableBehaviour)
    {
        
        trackableBehaviour.gameObject.AddComponent<AudioTracker>();
        CreateAudioSource();
        GameObject quad = GenerateQuad(Resources.Load<Texture2D>("Prefabs/speaker"));
        quad.GetComponent<Transform>().position = new Vector3(0,0.5f,0);

        FilePathDetect();
        string url =  "file://" + filePath;

        StartCoroutine(LoadARObject(filePath));
    }

    private void CreateAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    protected override IEnumerator LoadARObject(string url)
    {
        var request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
            audioSource.clip=DownloadHandlerAudioClip.GetContent(request);
        else
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

        request.Dispose();
    }

    public void StartPlay() 
    {
        audioSource.Play();
    }

    public void StopPlay() 
    {
        audioSource.Stop();
    }
}
