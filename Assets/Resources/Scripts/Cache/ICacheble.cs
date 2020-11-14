using System.Collections;
using System.Collections.Generic;

public interface ICacheble
{
    IEnumerator DownloadAndCache(int id);
    IEnumerator Cache(string folder, string fileName, string url);
}