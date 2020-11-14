using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestObj : MonoBehaviour
{
    public QuestsClass.Quest quest;

    public GameObject Title;
    public GameObject Description;
    public GameObject Button;

    private TextMeshProUGUI title;
    private TextMeshProUGUI description;

    void Start()
    {
        title = Title.GetComponent<TextMeshProUGUI>();
        description = Description.GetComponent<TextMeshProUGUI>();
        Button.GetComponent<Button>().onClick.AddListener(ChangeCurrentQuest);
        UpdateQuestInfo();
    }

    void UpdateQuestInfo()
    {
        title.text = quest.title;
        description.text = quest.description;
    }

    void ChangeCurrentQuest() 
    {
        AboutQuestPageManager.quest = quest;
    }
}
