using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMarker : MonoBehaviour
{
    public static string markerPath;
    public static string objectPath;

    public static bool isCustom = false;

    public static int GetObjectType() 
    {
        string ext = System.IO.Path.GetExtension(objectPath);
        Debug.Log(ext);
        if (ext == ".jpg" || ext == ".png")
            return 0;
        else if (ext == ".mp4")
            return 1;
        else if (ext == ".mp3")
            return 3;
        else if (ext == ".asset" || ext == "")
            return 4;
        else if (ext == ".animasset")
            return 7;
        else
            return -1;
    } 
}
