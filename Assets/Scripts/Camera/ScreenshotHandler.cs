using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    private ChangeVideoScreenshot _changeVideoScreenshoot;
    private Image _previewImage;

    public const string PHOTO_NAME = "Photo", GALLERY_PATH = "/../../../../ARProject/Photos";
    public static string fileName;
    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        _changeVideoScreenshoot = gameObject.GetComponent<ChangeVideoScreenshot>();
        _previewImage = _changeVideoScreenshoot.PreviewImage.GetComponent<Image>();
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);



            renderResult.ReadPixels(rect, 0, 0);
            renderResult.Apply();
            _previewImage.sprite = Sprite.Create(renderResult, rect, new Vector2(0.5f, 0.5f), 100);

            byte[] byteArray = renderResult.EncodeToPNG();

            string _screenshootPath = Path.Combine(Application.persistentDataPath, PHOTO_NAME + ".png");
            File.WriteAllBytes(_screenshootPath, byteArray);
            Debug.Log("Saved CameraScreenshot.png");
            SaveScreenShootToGallery();
            Recorder.ScreenRecorder.ShowToast("Saved screenshoot");
            //MoveScreenshootToARFolder(_screenshootPath);
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }


    public void SaveScreenShootToGallery()
    {
        StartCoroutine(SaveScreenShoot());
    }
    private IEnumerator SaveScreenShoot()
    {
        yield return null;
        DateTime now = DateTime.Now;
        fileName = "Photo_" + now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".png";
        while (!File.Exists(Application.persistentDataPath + "/" + PHOTO_NAME + ".png"))
            yield return null;
        if (!Directory.Exists(Application.persistentDataPath + GALLERY_PATH))
            Directory.CreateDirectory(Application.persistentDataPath + GALLERY_PATH);
        File.Copy(Application.persistentDataPath + "/" + PHOTO_NAME + ".png", Application.persistentDataPath + GALLERY_PATH + "/" + fileName);
        yield return null;
    }

    private void TakeScreenshot(int width, int height)
    {
        if (myCamera.transform.parent.gameObject.activeSelf == true)
        {
            myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
            takeScreenshotOnNextFrame = true;
        }
    }

    public static void TakeScreenshot_FullScreen()
    {
        instance.TakeScreenshot(Screen.width, Screen.height);
    }
}
