using UnityEngine;
using System.IO;

public class CacheChecker : MonoBehaviour
{
    public string GetTextFromCache(string fileName, string folder)
    {
        string cacheFilePath = Path.Combine(PathWorker.cachePathFolder, folder, fileName + ".txt");
        
        if (File.Exists(cacheFilePath))
        {
            return File.ReadAllText(cacheFilePath);
        }

        return null;
    }
    public static bool CheckFile(string path) 
    {
        return File.Exists(path);
    }
}
