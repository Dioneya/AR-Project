using System.Collections;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public GameObject LastAchievementContent;
    public GameObject WhereExitstUserScript;
    public void UpdatePage() 
    {
        this.gameObject.SetActive(true);
        if (GlobalVariables.tokenResponse != null)
        {
            StartCoroutine(UpdatePageCoroutine());
        }
    }
    IEnumerator UpdatePageCoroutine() 
    {
        WhereExitstUserScript.GetComponent<User>().UserInfoRefresh();
        yield return new WaitWhile(() => !WhereExitstUserScript.GetComponent<User>().isDoneUpdate);
        LastAchievementContent.GetComponent<ShowLastAchievements>().ShowAchievementList();
    }
}
