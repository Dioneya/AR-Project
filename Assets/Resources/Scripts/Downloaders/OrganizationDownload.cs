using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class OrganizationDownload : MonoBehaviour
{
    private static GameObject orgObj;
    private static InstitutionJsonLoader.InstitutionList institution;

    public static GameObject CreateDownload(InstitutionJsonLoader.InstitutionList inst, GameObject content)
    {
        bool isFromCache = false;
        foreach (GameObject i in OrganizationControl.organization_list) 
        {
            if (i.GetComponent<InstitutionInfoVariables>().id == inst.data.id) 
            {
                isFromCache = true;
                break;
            }  
        }
        if (!isFromCache)
        {
            orgObj = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "StickerPage", "Organization_download")));
            CreateOrg(inst, content);
        }
        else { orgObj = null; }
        return orgObj;
    }

    public static void CreateFormDelete(InstitutionJsonLoader.InstitutionList inst, GameObject content)
    {
        orgObj = Instantiate(Resources.Load<GameObject>(Path.Combine("Prefabs", "StickerPage", "Organization_delete")));
        CreateOrg(inst, content);
        FromCache();
    }

    private static void CreateOrg(InstitutionJsonLoader.InstitutionList inst, GameObject content) 
    {
        institution = inst;
        MarkerBlockInit(orgObj, content);
        InfoGenerate();
        OrganizationInformation();
        OrganizationControl.organization_list.Add(orgObj);
    }

    public static void FromCache() 
    {
        InstitutionInfoVariables info = orgObj.GetComponent<InstitutionInfoVariables>();
        InformationAboutOrganization organizationInformation = orgObj.GetComponent<InformationAboutOrganization>();

        info.cachePath = PathWorker.InstitutionPath(institution.data.id);
        organizationInformation.isAviable = true;

        LogoFromCache();
    }

    public static void LogoFromCache() 
    {
        string url = Path.Combine(PathWorker.InstitutionPath(institution.data.id), "logo.png");
        if (File.Exists(url))
        {
            InformationAboutOrganization organizationInformation = orgObj.GetComponent<InformationAboutOrganization>();
            RawImage image = organizationInformation.Image;
            Texture2D texture = new Texture2D(4, 4);
            texture.LoadImage(File.ReadAllBytes(url));
            image.texture = texture;
        }
        
    }

    private static void MarkerBlockInit(GameObject markerBlock, GameObject content)
    {
        markerBlock.SetActive(true);
        markerBlock.transform.SetParent(content.transform);
        markerBlock.transform.localPosition = new Vector3(markerBlock.transform.localRotation.x, markerBlock.transform.localRotation.y, 0);
        markerBlock.transform.localScale = new Vector3(1, 1, 1);
    }

    private static void InfoGenerate()
    {
        InstitutionInfoVariables info = orgObj.AddComponent<InstitutionInfoVariables>();
        info.nameInst = institution.data.title;
        info.description = institution.data.description;
        info.count = institution.data.markers.Count;
        info.id = institution.data.id;
    }

    private static void OrganizationInformation()
    {
        InformationAboutOrganization organizationInformation = orgObj.GetComponent<InformationAboutOrganization>();
        InstitutionInfoVariables info = orgObj.GetComponent<InstitutionInfoVariables>();

        organizationInformation.OrganizationName.text = info.nameInst;
        organizationInformation.OrganizationMarkersCount.text = info.count.ToString();
        organizationInformation.OrganizationAddress.text = info.description;
        organizationInformation.Institution = institution;
    }
}
