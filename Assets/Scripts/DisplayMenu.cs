using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    public static bool isShowed = true;
    private Animation menuAnimation;
    private static Animation menuAnim;
    void Start()
    {
        menuAnimation = GetComponent<Animation>();
        menuAnim = menuAnimation;
    }

    public static void PlayAnimation(string AnimName)
    {
        menuAnim.CrossFade(AnimName);
        menuAnim.Play(AnimName);
        isShowed = !isShowed;
    }
    public static void Show()
    {
        if (isShowed != true)
            PlayAnimation("ShowMenu");
    }
    public static void Hide()
    {
        if (isShowed == true)
            PlayAnimation("HideMenu");
    }

    public static void ToggleMenu()
    {
        if (isShowed == true)
            Hide();
        else
        {
            Show();
        }
    }
}
