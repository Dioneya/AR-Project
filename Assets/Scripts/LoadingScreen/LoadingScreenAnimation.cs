using System.Collections;
using UnityEngine;
public class LoadingScreenAnimation : MonoBehaviour
{
    [SerializeField] private Animation LoadingText;
    public static bool isPlayAnimation = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextAnimation());
    }

    IEnumerator TextAnimation()
    {
        if (isPlayAnimation == true)
        {
            LoadingText.Play();
            yield return new WaitForSeconds(2f);
            StartCoroutine(TextAnimation());
        }
    }
}
