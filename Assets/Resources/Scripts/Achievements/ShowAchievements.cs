using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShowAchievements : MonoBehaviour
{
    protected List<GameObject> achievementsList = new List<GameObject>();
    protected void CreateAchievement(ref GameObject instance, UserClass.Achievement achievement)
    {
        #region Создание и позиционирование блока ачивки
        instance.transform.SetParent(this.transform);
        instance.transform.localPosition = new Vector3(instance.transform.localPosition.x, instance.transform.localPosition.y, 0);
        instance.transform.localScale = new Vector3(1, 1, 1);
        #endregion

        #region Имя и описание ачивки
        GameObject achivName = instance.transform.Find("Text (TMP)").gameObject;
        achivName.GetComponent<TextMeshProUGUI>().text = achievement.title;

        GameObject achivDesc = instance.transform.Find("Description").gameObject;
        achivDesc.GetComponent<TextMeshProUGUI>().text = achievement.description;
        #endregion

        #region Установка изображения ачивки 
        GameObject image_obj = instance.transform.Find("Image").gameObject;
        RawImage rawImage = image_obj.GetComponent<RawImage>();
        StartCoroutine(ImgDownload.DownloadImage(achievement.ach_image_url, rawImage));
        #endregion

        achievementsList.Add(instance); //добавим в лист ссылок на объекты ачивок
    }

    protected void ClearSpace() 
    {
        for (int i = 0; i < achievementsList.Count; i++) { 
            Destroy(achievementsList[i]); 
        }
        achievementsList.Clear();
    }

    public virtual void ShowAchievementList() { }
    protected virtual IEnumerator Show() { yield return null; }

    public IEnumerator GetProgress(int id, GameObject progressBar)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/achievements/progress/" + id);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
        www.SetRequestHeader("Authorization", "Bearer " + GlobalVariables.tokenResponse.access_token);
        yield return www.SendWebRequest();


        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AchievementProgressClass.Achievement achievement = JsonUtility.FromJson<AchievementProgressClass.Achievement>(www.downloadHandler.text);

            if (www.downloadHandler.text == null)
            {
                Debug.Log("Получить данные не удалось!");
            }
            else
            {
                Debug.Log("Данные пользователя получены!");
                if (achievement.current_progress != 0)
                {
                    progressBar.GetComponent<Slider>().value = achievement.needed_progress / achievement.current_progress;
                }
                else 
                {
                    progressBar.GetComponent<Slider>().value = 0;
                }
            }
        }
    }
}
