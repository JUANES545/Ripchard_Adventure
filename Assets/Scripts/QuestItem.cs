using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager _questManager;
    public string itemName;
    void Start()
    {
        _questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_questManager.quests[questID].gameObject.activeInHierarchy && 
                !_questManager.questCompleted[questID])
            {
                _questManager.itemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }
}
