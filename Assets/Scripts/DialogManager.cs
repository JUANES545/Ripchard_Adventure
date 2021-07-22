using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    [SerializeField] private Text npcNameText;
    public string[] dialogLines;
    public int currentDialogLine;
    public String npcName;
    public bool dialogFinished;
    
    public bool dialogActive;
    private PlayerController _playerController;
    public NPCDialog _npcDialog;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _npcDialog = FindObjectOfType<NPCDialog>();
        dialogFinished = false;
    }

    private void FixedUpdate()
    {
        if (currentDialogLine >= dialogLines.Length)
        {
            dialogActive = false;
            dialogBox.SetActive(false);
            
            _playerController.playerTalking = false;
            
            if (currentDialogLine != 0)
            {
                dialogFinished = true;
            }
            currentDialogLine = 0;
            
        }
        else
        {
            dialogText.text = dialogLines[currentDialogLine];
            npcNameText.text = npcName;
            dialogFinished = false;
        }
    }

    public void ShowDialog(string[] lines)
    {
        dialogActive = true;
        dialogBox.SetActive(true);
        currentDialogLine = 0;
        dialogLines = lines;

        _playerController.playerTalking = true;
    }

    public void NextDialog2()
    {
        if (!dialogActive) return;
        currentDialogLine++;
    }

    public void NextDialog(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            NextDialog2();
        }
    }

    public void EnterDialog(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _npcDialog.EnterDialog();
        }
    }
}
