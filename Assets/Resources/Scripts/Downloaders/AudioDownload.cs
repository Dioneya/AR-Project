using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class AudioDownload : MonoBehaviour, IDownloadeble, IPlayable
{
    public string url;
    AudioSource audioSource;

    public string SetUrl(string filePath) {
        return url = "file://" + filePath;
    }
    public void StartDownload(string filePath, DataSetTrackableBehaviour trackableBehaviour)
    {
        trackableBehaviour.gameObject.AddComponent<AudioTracker>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        quad.GetComponent<Transform>().Rotate(90, 0, 0);
        quad.transform.parent = this.transform;
        quad.GetComponent<Transform>().position = new Vector3(0,0.5f,0);
        Texture2D texture = Resources.Load<Texture2D>("Prefabs/speaker");

        quad.GetComponent<Renderer>().material.mainTexture = texture;

        StartCoroutine(LoadAudioFromServer(SetUrl(filePath), AudioType.MPEG));
    }

    IEnumerator LoadAudioFromServer(string url, AudioType audioType)
    {
        var request = UnityWebRequestMultimedia.GetAudioClip(url, audioType);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            audioSource.clip=(DownloadHandlerAudioClip.GetContent(request));
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);
        }

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
