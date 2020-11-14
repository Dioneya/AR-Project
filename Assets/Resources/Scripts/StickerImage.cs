using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StickerImage : MonoBehaviour
{
    [SerializeField] private GameObject StickerPrefab;
    [SerializeField] private GameObject HintBlock;
    [SerializeField] private RawImage HintImage;
    [SerializeField] private TextMeshProUGUI HintDescription;
    // Start is called before the first frame update
    public void StartLoad(InstitutionJsonLoader.InstitutionList institution)
    {
        
        for (int i = 0; i < institution.data.markers.Count; i++ ) 
        {
            #region Создание стикера и первоначальная настройка
            GameObject image = Instantiate(StickerPrefab);
            image.SetActive(true);
            image.name = "Sticker Image " + institution.data.markers[i].id;
            image.transform.SetParent(transform);
            #endregion

            #region ShowHintImage
            ShowHintImage showbigsticker = image.GetComponent<ShowHintImage>();
            showbigsticker.HintImage = HintImage;
            showbigsticker.HintBlock = HintBlock;
            showbigsticker.HintDescription = HintDescription;
            showbigsticker.text = institution.data.markers[i].description;
            #endregion

            #region Настройка RawImage
            RawImage rawImage = image.GetComponent<RawImage>();
            image.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            image.transform.localPosition = new Vector3(image.transform.localPosition.x, image.transform.localPosition.y, 0);
            GlobalVariables.Images.Add(image);
            #endregion

            string cacheFilePath = PathWorker.CacheFilePath(i, AR_Objects.Type.Sticker);

            StartCoroutine(ImgDownload.DownloadImage("file://"+cacheFilePath, rawImage));
        }
        
    }


}
