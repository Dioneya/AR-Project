using UnityEngine;
using UnityEngine.UI;

public class ChangeInstitution : MonoBehaviour
{
    private InstitutionInfoVariables _institutionInfoVariables;
    void Start()
    {
        _institutionInfoVariables = GetComponentInParent<InstitutionInfoVariables>();
        GetComponent<Button>().onClick.AddListener(delegate { ChangeName(_institutionInfoVariables.count, _institutionInfoVariables.name, _institutionInfoVariables.description); });
    }

    void ChangeName(int countInstitution, string name, string description)
    {
        GlobalVariables.countInst = countInstitution;
        GlobalVariables.nameInst = name;
        GlobalVariables.descInst = description;
    }
}
