using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class QuestTrigger : MonoBehaviour
{
    private QuestManager _questManager;
    public int questID;
    public bool startPoint, endPoint;
    
    void Start()
    {
        _questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_questManager.questCompleted[questID]) //si se desea repetir varias veces eliminar el if
            {
                if (startPoint && !_questManager.quests[questID].gameObject.activeInHierarchy)
                {
                    _questManager.quests[questID].gameObject.SetActive(true);
                    _questManager.quests[questID].StartQuest();
                }

                if (endPoint && _questManager.quests[questID].gameObject.activeInHierarchy)
                {
                    _questManager.quests[questID].CompleteQuest();
                }
            }
        }
    }
}
