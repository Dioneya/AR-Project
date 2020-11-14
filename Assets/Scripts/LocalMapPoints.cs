using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LocalMapPoints : MonoBehaviour
{
    [SerializeField] private GameObject PointPrefab;
    [SerializeField] private GameObject HintBlock;
    [SerializeField] private RawImage HintImage;
    [SerializeField] private TextMeshProUGUI HintDescription;
    // Start is called before the first frame update
    void Start()
    {
        GameObject image = Instantiate(PointPrefab);
        image.SetActive(true);
        image.transform.SetParent(transform);
        image.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        image.GetComponent<RectTransform>().localPosition = new Vector3(1, 1, 1);
        ShowHintImage showbigsticker = image.GetComponent<ShowHintImage>();
        showbigsticker.HintImage = HintImage;
        showbigsticker.HintBlock = HintBlock;
        showbigsticker.HintDescription = HintDescription;
    }
}
