using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ShowAllAchievements : ShowAchievements
{
    public AllAchievementsClass.AllAchievements allAchievements;
    public override void ShowAchievementList()
    {
        StartCoroutine(AchievementList());
    }

    private void Start()
    {
        ShowAchievementList();
    }

    protected override IEnumerator Show() 
    {
        ClearSpace();
        foreach (UserClass.Achievement achievement in allAchievements.data) 
        {
            GameObject instance = (GameObject)Instantiate(Resources.Load(Path.Combine("Prefabs", "Achievements", "Achievement")));
            CreateAchievement(ref instance, achievement);

            GameObject Progress = instance.transform.Find("ProgressBlock").gameObject;
            GameObject Slider = Progress.transform.Find("Slider").gameObject;
            StartCoroutine(GetProgress(achievement.id, Slider));
        }
        yield return null;
    }

    IEnumerator AchievementList()
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/achievements");
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            allAchievements = JsonUtility.FromJson<AllAchievementsClass.AllAchievements>(www.downloadHandler.text);
            if (allAchievements == null)
            {
                Debug.Log("Получить данные не удалось!");
            }
            else
            {
                Debug.Log("Данные пользователя получены!");
                StartCoroutine(Show());
            }
        }
    }
}
