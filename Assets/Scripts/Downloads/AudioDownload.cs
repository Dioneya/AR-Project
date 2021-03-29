using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;
using System;

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
        string url =  "file:///" + filePath;

        StartCoroutine(LoadARObject(url));
    }

    private void CreateAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    protected override IEnumerator LoadARObject(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
            }
        }
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
