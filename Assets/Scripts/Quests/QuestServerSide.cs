using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

/*
=== Документация по работе с данным файлом ===
=== Взаимодействие с сервером по данным квеста === 

Полученный данные квеста по id
StartCoroutine(Quest.QuestInfo(id_квеста, result => переменная_в_которую_нужно_вернуть_значение = result)); 

Получить список доступных квестов
StartCoroutine(QuestList( result => переменная_в_которую_нужно_вернуть_значение = result )); 

Взять квест по id
StartCoroutine(TakeQuest( GlobalVariables.tokenResponse.access_token , id_квеста ));

Бросить квест по id 
StartCoroutine(DropQuest( GlobalVariables.tokenResponse.access_token, id_квеста ));

Получить список выполненных квестов 
StartCoroutine(CompletedQuests( GlobalVariables.tokenResponse.access_token , result => переменная_в_которую_нужно_вернуть_значение = result ));

Получить список активных квестов
StartCoroutine( ActiveQuests ( GlobalVariables.tokenResponse.access_token , result => переменная_в_которую_нужно_вернуть_значение = result ));

Получить прогресс квеста по id 
StartCoroutine(QuestProgress(GlobalVariables.tokenResponse.access_token, id_квеста , result => переменная_в_которую_нужно_вернуть_значение = result));
*/
public class QuestServerSide : MonoBehaviour
{
    [Serializable]
    public class Message 
    {
        public string message;
    }

    #region Просмотр информации по квесту
    public static IEnumerator QuestInfo(int id, Action<QuestsClass.Quest> cb)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/" + Convert.ToString(id));
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            QuestsClass.Quest quest = JsonUtility.FromJson<QuestsClass.Quest>(www.downloadHandler.text);
            
            Debug.Log(quest == null ? "Получить данные квеста не удалось!" : "Данные квеста получены!");

            cb(quest);
        }
    }
    #endregion

    #region Список всех доступных квестов
    public static IEnumerator QuestList(Action<QuestsClass.QuestsList> cb)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/");
        www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            QuestsClass.QuestsList quests = JsonUtility.FromJson<QuestsClass.QuestsList>(www.downloadHandler.text);

            Debug.Log(quests == null ? "Получить данные не удалось!" : "Данные пользователя получены!");

            cb(quests);
        }
    }
    #endregion

    #region Взять выбранный квест
    public static IEnumerator TakeQuest(string token, int id)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/take/" + Convert.ToString(id));
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
            Message TakeMessage = JsonUtility.FromJson<Message>(www.downloadHandler.text);

            Debug.Log(TakeMessage.message);
        }
    }
    #endregion

    #region Бросить выбранный квест

    public static IEnumerator DropQuest(string token, int id)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/drop/" + Convert.ToString(id));
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
            Message DropMessage = JsonUtility.FromJson<Message>(www.downloadHandler.text);

            Debug.Log(DropMessage.message);
        }
    }

    #endregion

    #region Список выполненных квестов
    public static IEnumerator CompletedQuests(string token, Action<QuestsClass.QuestsList> cb)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/completed/");
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
            QuestsClass.QuestsList completedQuests = JsonUtility.FromJson<QuestsClass.QuestsList>(www.downloadHandler.text);

            Debug.Log(completedQuests == null ? "Список получить не удалось!" : "Список получен!");

            cb(completedQuests);
        }
    }
    #endregion

    #region Список активных квестов
    public static IEnumerator ActiveQuests(string token, Action<QuestsClass.QuestsList> cb)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/completed/");
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
            QuestsClass.QuestsList activeQuests = JsonUtility.FromJson<QuestsClass.QuestsList>(www.downloadHandler.text);

            Debug.Log(activeQuests == null ? "Список получить не удалось!" : "Список получен!");

            cb(activeQuests);
        }
    }
    #endregion

    #region Прогресс по квесту
    public static IEnumerator QuestProgress(string token, int id, Action<QuestsClass.Progress> cb)
    {
        UnityWebRequest www = UnityWebRequest.Get(GlobalVariables.link + "/api/quests/progress/" + Convert.ToString(id));
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
            QuestsClass.Progress progress = JsonUtility.FromJson<QuestsClass.Progress>(www.downloadHandler.text);

            cb(progress);
            //Debug.Log(DropMessage);
        }
    }
    #endregion
}
