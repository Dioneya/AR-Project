using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GradientBackground : MonoBehaviour
{
    [SerializeField] private Color from;
    [SerializeField] private Color to;
    public RawImage img;

    private Texture2D backgroundTexture;

    void Awake()
    {
        SetColor(from, to);
    }

    public void SetColor(Color color1, Color color2)
    {
        backgroundTexture = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        backgroundTexture.SetPixels(new Color[] { color2, color1 });
        backgroundTexture.Apply();
        img.texture = backgroundTexture;
    }
    public static void SetColor(RawImage img, Color color1, Color color2) {
        Texture2D backgroundTexture = new Texture2D(1, 2);
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        backgroundTexture.SetPixels(new Color[] { color2, color1 });
        backgroundTexture.Apply();
        img.texture = backgroundTexture;
    }
}
