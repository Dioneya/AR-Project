
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Token : MonoBehaviour
{
    private readonly string _uri = GlobalVariables.link + "/api/auth/refresh";
    #region Обновление Тоукена
    void Start() 
    {
        UserConfig.ReadUser();
        if (GlobalVariables.tokenResponse!=null) 
        {
            StartCoroutine(TokenUpdate());
        }
       
    }
    public IEnumerator TokenUpdate()
    {
        if (GlobalVariables.tokenResponse.refresh_token!=null) 
        {
            List<IMultipartFormSection> formSections = new List<IMultipartFormSection>();
            formSections.Add(new MultipartFormDataSection("refresh_token", GlobalVariables.tokenResponse.refresh_token));

            UnityWebRequest www = UnityWebRequest.Post(_uri, formSections);
            //www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");

            yield return www.SendWebRequest();

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            }
            else
            {
                GlobalVariables.tokenResponse = JsonUtility.FromJson<TokenClass.TokenResponse>(www.downloadHandler.text);

                if (GlobalVariables.tokenResponse == null)
                {
                    Debug.Log("Обновить не удалось!");
                }
                else
                {
                    // Сохраняем токен в настройках
                    Debug.Log("Токен Обновлён!");
                    UserConfig.WriteUser();
                    //Debug.LogWarning(tokenResponse.refresh_token);
                }
            }
        } 
    }
    #endregion

    

}
