using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchOrg : MonoBehaviour
{
    [SerializeField] private InstitutionsManager manager;
    [SerializeField] private TMP_InputField text;

    public void Search()
    {
        foreach(GameObject org in manager.organizationsList)
        {
            org.SetActive(org.GetComponent<Organization>().institution.title.Contains(text.text));   
        }
    }

}
