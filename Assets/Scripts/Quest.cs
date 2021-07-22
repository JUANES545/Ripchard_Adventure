using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questID;
    public string startText, completeText;
    [SerializeField] private string kindOfMision;
    [SerializeField] private int expQuest = 10;
    [SerializeField] private bool needsItem;
    [SerializeField] private string itemNeeded;

    private QuestManager _questManager;
    private CharacterStats _characterStats;

    public bool needsEnemy;
    public string enemyName;
    public int numberOfEnemies;
    private int enemiesKilled;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (needsItem && _questManager.itemCollected.Equals(itemNeeded))
        {
            _questManager.itemCollected = null;
            CompleteQuest();
        }

        if(needsEnemy && _questManager.enemyKilled != null){
            if(_questManager.enemyKilled.Equals(enemyName)){
                _questManager.enemyKilled = null;
                enemiesKilled++;
                if(enemiesKilled >= numberOfEnemies){
                    CompleteQuest();
                }	
            }
        }
    }

    public void StartQuest()
    {
        _questManager = FindObjectOfType<QuestManager>();
        StartCoroutine(ShowQuest());
    }

    private IEnumerator ShowQuest()
    {
        yield return new WaitForSeconds(1f);
        _questManager.ShowQuestText(startText, kindOfMision);
    }

    public void CompleteQuest()
    {
        _characterStats = FindObjectOfType<CharacterStats>();
        _questManager.ShowQuestText(completeText, kindOfMision);
        _questManager.questCompleted[questID] = true;
        gameObject.SetActive(false); //si eliminamos esta linea podemos repetir la misión
        _characterStats.AddExperience(expQuest);
        
    }
}
