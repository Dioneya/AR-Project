using System.Collections;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class AboutQuestPageManager : MonoBehaviour
{
    public static QuestsClass.Quest quest;
    public QuestsClass.Progress progress;

    public GameObject Content;
    public GameObject Title;
    public GameObject Description;
    public GameObject Pivot;

    private TextMeshProUGUI title;
    private TextMeshProUGUI description;

    void Start()
    {
        title = Title.GetComponent<TextMeshProUGUI>();
        description = Description.GetComponent<TextMeshProUGUI>();
        //Pivot = Resources.Load<GameObject>(Path.Combine("Prefabs", "QuestPage", "Quest Condition"));
        Debug.Log(Pivot);
    }
    void ChangeTitile() 
    {
        title.text = quest.title;
        description.text = quest.description;
    }

    void OnEnable() 
    {
        StartCoroutine(InfoUpdate());
    }
    IEnumerator InfoUpdate() 
    {
        yield return new WaitWhile(() => quest == null);
        ChangeTitile();
        StartCoroutine(QuestProgressPivot());
    }

    IEnumerator QuestProgressPivot() 
    {
        progress = null;
        Clear();
        StartCoroutine(QuestServerSide.QuestProgress(GlobalVariables.tokenResponse.access_token, quest.id, result => progress = result));
        yield return new WaitWhile(()=>progress == null); 
        foreach (QuestsClass.ProgressPoint point in progress.points) 
        {
            Pivot.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = point.title;
            Pivot.transform.GetChild(1).gameObject.GetComponent<Toggle>().isOn = point.is_completed;
            GameObject inst = Instantiate(Pivot);
            inst.transform.SetParent(Content.transform,false);
            inst.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
            inst.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
        }

    }

    private void Clear()
    {
        var children = new List<GameObject>();
        foreach (Transform child in Content.transform) 
            children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
    }
}
