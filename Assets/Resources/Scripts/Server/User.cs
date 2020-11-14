using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class User : MonoBehaviour
{
    private readonly string _uri = GlobalVariables.link + "/api/auth/me";
    public bool isDoneUpdate = false;
    #region Информация о пользователе
    public void UserInfoRefresh() 
    {
       
        StartCoroutine(UserInfo(GlobalVariables.tokenResponse.access_token));
    }
    IEnumerator UserInfo(string token)
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        UnityWebRequest www = UnityWebRequest.Get(_uri);
        //www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
        www.SetRequestHeader("Authorization", "Bearer " + token);
        yield return www.SendWebRequest();


        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            GlobalVariables.user = JsonUtility.FromJson<UserClass.User>(www.downloadHandler.text);

            if (GlobalVariables.user == null)
            {
                Debug.Log("Получить данные не удалось!");
            }
            else
            {
                Debug.Log("Данные пользователя получены!");
            }
        }
        isDoneUpdate = true;
    }

    #endregion

    #region Список ачивок пользователя
    public IEnumerator UserAchivments()
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/achievements/complete");
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
            //Profile.userAchivments = JsonUtility.FromJson<Profile.UserAchivments>(www.downloadHandler.text);

            if (GlobalVariables.user == null)
            {
                Debug.Log("Получить данные не удалось!");
            }
            else
            {
                Debug.Log("Данные пользователя получены!");
            }
        }
    }
    #endregion
}
