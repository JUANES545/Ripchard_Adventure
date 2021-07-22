using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class UIManager : MonoBehaviour
{
    [SerializeField] Slider playerHealthBar;
    [SerializeField] Text playerHealthText;
    [SerializeField] Slider playerExpBar;
    [SerializeField] Text playerExpText;
    [SerializeField] Text playerLevelText;
    

    public HealthManager playerHealthManager;
    public CharacterStats PlayerStatsManager;

    private void FixedUpdate()
    {
        PlayerHealthBar();
        PlayerExpBar();
    }

    private void PlayerHealthBar()
    {
        //por si subimos de nivel
        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.currentHealth;

        StringBuilder sb = new StringBuilder("HP: ");
        sb.Append(playerHealthManager.currentHealth);
        sb.Append("/");
        sb.Append(playerHealthManager.maxHealth);
        playerHealthText.text = sb.ToString();
    }

    private void PlayerExpBar()
    {
        playerExpBar.maxValue = PlayerStatsManager.expToLevelUp[PlayerStatsManager.currentLevel];
        playerExpBar.value = PlayerStatsManager.currentExp;

        StringBuilder sb = new StringBuilder("Exp: ");
        sb.Append(PlayerStatsManager.currentExp);
        sb.Append("/");
        sb.Append(PlayerStatsManager.expToLevelUp[PlayerStatsManager.currentLevel]);
        playerExpText.text = sb.ToString();
        StringBuilder sb1 = new StringBuilder("Niv. ");
        sb1.Append(PlayerStatsManager.currentLevel.ToString());
        playerLevelText.text = sb1.ToString();

    }
}
