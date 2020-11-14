using TMPro;
using UnityEngine;

public class OrganizationHeader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI OrganizationName;
    [SerializeField] private TextMeshProUGUI OrganizationMarkers;
    [SerializeField] private TextMeshProUGUI OrganizationAddress;

    public void ChangeFields() {
        OrganizationName.text = GlobalVariables.nameInst;
        OrganizationMarkers.text = GlobalVariables.countInst.ToString();
        OrganizationAddress.text = GlobalVariables.descInst;
    }
}
