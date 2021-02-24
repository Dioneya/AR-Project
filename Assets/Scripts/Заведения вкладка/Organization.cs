using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Organization : MonoBehaviour
{
    public InstitutionJsonLoader.InstitutionObj institution;
    [SerializeField] private RawImage image;
    [SerializeField] private TextMeshProUGUI name, description;
    [SerializeField] private Button NextBtn;


    private void Start()
    {
        NextBtn.onClick.AddListener(() => OranizationDetails.imageOrg = image.texture);
        NextBtn.onClick.AddListener(() => OranizationDetails.inst = institution);
        NextBtn.onClick.AddListener(() => InstitutionsManager.PickOrganization.Invoke());
    }
    public void SetParam(InstitutionJsonLoader.InstitutionObj _institution)
    {
        institution = _institution;
        
        StartCoroutine(ImgDownload.DownloadImage(GlobalVariables.link+institution.image,image));
        name.text = institution.title;
        description.text = institution.description;
    }
}
