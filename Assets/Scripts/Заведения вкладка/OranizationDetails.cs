using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class OranizationDetails : MonoBehaviour
{
    public static InstitutionJsonLoader.InstitutionObj inst;
    public static Texture imageOrg = null;
    [Header("Карточка Заведения")]
    [SerializeField] private TextMeshProUGUI organizationName, organizationDescription;
    [SerializeField] private RawImage image;
    [SerializeField] private Button buttonDownload, buttonScan;
    [Header("Основная часть")]
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject delete;
    



    private void Start()
    {
        buttonDownload.onClick.AddListener(OnButtonDownloadClick);
        buttonScan.onClick.AddListener(OnButtonDownloadClick);
    }

    private void OnEnable()
    {
        LoadInfo();
    }

    public void LoadInfo() 
    {
        image.texture = imageOrg;
        organizationName.text = inst.title;
        organizationDescription.text = inst.description;
        description.text = inst.description;

        bool isCached = Directory.Exists(PathWorker.InstitutionPath(inst.id));

        buttonDownload.gameObject.SetActive(!isCached);
        buttonScan.gameObject.SetActive(isCached);
        delete.SetActive(isCached);
    }

    public void DeleteCache()
    {
        Directory.Delete(PathWorker.InstitutionPath(inst.id), true);
    }

    private void OnButtonDownloadClick() 
    {
        GlobalVariables.checkedID = inst.id;
        SceneManager.LoadScene(1);
    }
}
