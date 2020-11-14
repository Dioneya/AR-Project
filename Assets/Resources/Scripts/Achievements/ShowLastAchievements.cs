using UnityEngine;
using System.Collections;
using System.IO;

public class ShowLastAchievements : ShowAchievements
{
    public override void ShowAchievementList()
    {
        StartCoroutine(Show());
    }
    protected override IEnumerator Show()
    {
        yield return new WaitWhile(() => GlobalVariables.user == null);
        ClearSpace();

        foreach (UserClass.Achievement achievement in GlobalVariables.user.achievements)
        {
            GameObject instance = (GameObject)Instantiate(Resources.Load(Path.Combine("Prefabs", "ProfilePage", "achievement_")));
            CreateAchievement(ref instance, achievement);      
        }
    }

}
