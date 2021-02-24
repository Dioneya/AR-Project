using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class OrganizationStickers : MonoBehaviour
{
    [SerializeField] GameObject stickerObj;
    List<GameObject> stickers = new List<GameObject>();


    private void OnEnable() {
        Clear();
        LoadStickers();
    }

    void LoadStickers()
    {
        foreach(MarkerJsonLoader.Marker marker in OranizationDetails.inst.markers)
        {
            GameObject sticker = Instantiate(stickerObj, transform);
            string cachePath = Path.Combine(PathWorker.MarkerStrPathCustom(marker.id-1, OranizationDetails.inst),"sticker.png");
            string urlImage = CacheChecker.CheckFile(cachePath) ? "file:///"+cachePath : GlobalVariables.link+marker.sticker_image;
            StartCoroutine(ImgDownload.DownloadImage(urlImage,sticker.GetComponent<RawImage>()));
            stickers.Add(sticker);
        }   
    }

    void Clear()
    {
        foreach(GameObject sticker in stickers)
            Destroy(sticker);
        
        stickers.Clear();
    }
}
