using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialog : MonoBehaviour
{
    [SerializeField] private string npcName;
    [SerializeField] string[] dialog;
    [SerializeField] private bool requiredQuest;
    [SerializeField] private int questID; 
    
    private DialogManager _manager;
    public bool playerInTheZone;

    private QuestManager _questManager;
    
    void Start()
    {
        _manager = FindObjectOfType<DialogManager>();
        _questManager = FindObjectOfType<QuestManager>();
    }

    private void FixedUpdate()
    {
        if (playerInTheZone && requiredQuest && _manager.dialogFinished)
        {
            if (!_questManager.quests[questID].gameObject.activeInHierarchy && !_questManager.questCompleted[questID])
            {
                _questManager.quests[questID].gameObject.SetActive(true);
                _questManager.quests[questID].StartQuest();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTheZone = true;
            _manager._npcDialog = this;
            _manager.npcName = npcName;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInTheZone = false;
        _manager._npcDialog = this;
    }

    public void EnterDialog()
    {
        if (playerInTheZone)
        {
            _manager.ShowDialog(dialog);
            if (gameObject.GetComponentInParent<NPCMovement>() != null)
            {
                gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
            }
        }
    }

}
