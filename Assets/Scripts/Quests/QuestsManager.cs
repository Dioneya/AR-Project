using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : MonoBehaviour
{
    public GameObject ContentForQuestsList;

    public QuestsClass.QuestsList questsList;
    private GameObject Quest ;
    private QuestObj questInfo;
    private DisplayElements displayElements;
    void Start()
    {
        Quest = Resources.Load<GameObject>(Path.Combine("Prefabs", "QuestPage", "Quest"));
        questInfo = Quest.GetComponent<QuestObj>();
        displayElements = GetComponent<DisplayElements>();

        DownloadInfoPage();
    }
    //Метод для добавления загрузки при старте и обновлении стр 
    public void DownloadInfoPage() 
    {
        Clear();
        StartCoroutine(ShowQuestsList());
    }

    private IEnumerator ShowQuestsList() 
    {
        yield return StartCoroutine(QuestServerSide.QuestList(result => questsList = result));

        for (int i = 0; i < questsList.data.Count; i++) 
        {
            questInfo.quest = questsList.data[i];
            GameObject inst = Instantiate(Quest);
            inst.GetComponent<QuestObj>().Button.GetComponent<Button>().onClick.AddListener(displayElements.ShowAndHideElements);
            inst.transform.SetParent(ContentForQuestsList.transform, false);
        }
    }

    private void Clear() 
    {
        var children = new List<GameObject>();
        foreach (Transform child in ContentForQuestsList.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }

}
