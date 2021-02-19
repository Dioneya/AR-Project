using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
public class ServerDownloader : MonoBehaviour
{
    private Dictionary<ArObjects.Type, ICacheble> dict ;
    [SerializeField] TextMeshProUGUI loadText;
    public double progress = 0, total = 0;
    void Awake()
    {
        dict = new Dictionary<ArObjects.Type, ICacheble>()
        {
            {ArObjects.Type.Image, gameObject.AddComponent<AR_image>()},
            {ArObjects.Type.Audio, gameObject.AddComponent<AR_audio>()},
            {ArObjects.Type.Video,gameObject.AddComponent<AR_video>()},
            {ArObjects.Type.AssetBundle, gameObject.AddComponent<AR_assetBundle>()},
            {ArObjects.Type.Text, gameObject.AddComponent<AR_text>()},
            {ArObjects.Type.Logo, gameObject.AddComponent<AR_logo>()},
            {ArObjects.Type.Sticker, gameObject.AddComponent<AR_sticker>()},
            {ArObjects.Type.Marker, gameObject.AddComponent<AR_marker>()},
            {ArObjects.Type.AnimatedAssetBundle, gameObject.AddComponent<AR_animatedAssetBundle>()}
        };
    }

    public IEnumerator Download(InstitutionJsonLoader.Institution institution)
    {
        total = 1+(institution.data.markers.Count*3);

        yield return StartCoroutine(dict[ArObjects.Type.Logo].DownloadAndCache(institution.data.id));
        AddProcent();
        foreach(MarkerJsonLoader.Marker marker in institution.data.markers)
        {
            Debug.LogWarning(GlobalVariables.link + marker.image_set[0].url+" "+marker.id);
            yield return StartCoroutine(dict[ArObjects.Type.Sticker].DownloadAndCache(marker.id-1));
            AddProcent();
            yield return StartCoroutine(dict[ArObjects.Type.Marker].DownloadAndCache(marker.id-1));
            AddProcent();
            yield return StartCoroutine(dict[(ArObjects.Type)marker.a_r_object.object_type.value].DownloadAndCache(marker.id-1));
            AddProcent();
        }

        SceneManager.LoadScene(2);
    }

    void AddProcent()
    {
        progress++;
        loadText.text = "Загрузка " + System.Math.Round((double)(progress/total*100)) + "%";
    }
}
