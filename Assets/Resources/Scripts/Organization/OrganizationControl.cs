using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OrganizationControl : MonoBehaviour
{
    public static List<GameObject> organization_list = new List<GameObject>();
    public static GameObject content;
    public GameObject search_obj;

    private Search search;
    private MarkerInfo cache_org;

    private void Start() 
    {
        content = this.gameObject;
        cache_org = this.gameObject.GetComponent<MarkerInfo>();
        search = search_obj.gameObject.GetComponent<Search>();

        Update_Info();
    }

    public void Update_Info() 
    {
        ClearObj();
        cache_org.OrgDownload();
        search.DownloadFromDict();
    }

    private void ClearObj()
    {
        foreach (GameObject i in organization_list)
        {
            Destroy(i);
        }
        organization_list.Clear();
    }

}
