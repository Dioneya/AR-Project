using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OranizationDetails : MonoBehaviour
{
    public static InstitutionJsonLoader.InstitutionObj inst;
    public static Texture imageOrg = null;
    [Header("Карточка Заведения")]
    [SerializeField] private TextMeshProUGUI organizationName, organizationDescription;
    [SerializeField] private RawImage image;
    [SerializeField] private Button button;
    [Header("Основная часть")]
    [SerializeField] private TextMeshProUGUI description;
    

    private void Start()
    {
        button.onClick.AddListener(OnButtonDownloadClick);
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
    }

    private void OnButtonDownloadClick() 
    {
        GlobalVariables.checkedID = inst.id;
        SceneManager.LoadScene(1);
    }
}
