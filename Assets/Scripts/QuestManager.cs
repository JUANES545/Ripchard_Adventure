using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questBox;
    [SerializeField] private Text questText;
    [SerializeField] private Text kindQuestText;
    
    public Quest[] quests;
    public bool[] questCompleted;

    private DialogManager _dialogManager;

    public string itemCollected;
    public string enemyKilled;

    private void Start()
    {
        questCompleted = new bool[quests.Length];
        _dialogManager = FindObjectOfType<DialogManager>();
    }

    public void ShowQuestText(string quest, string kindQuest)
    {
        questBox.SetActive(true);
        questText.text = quest;
        kindQuestText.text = kindQuest;
        StartCoroutine(DestroyQuestText());
    }

    private IEnumerator DestroyQuestText()
    {
        yield return new WaitForSeconds(5f);
        questBox.SetActive(false);
    }
    
    /*public void ShowQuestText(string questText)
    {
        string[] dialogLines = new string[]
        {
            questText
        };
        _dialogManager.ShowDialog(dialogLines);
    }*/
}
