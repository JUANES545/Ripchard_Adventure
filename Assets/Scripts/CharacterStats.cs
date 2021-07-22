using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int currentLevel;
    public int currentExp;
    
    public int[] expToLevelUp;
    public int[] hPLevels, strengthLevels, defenseLevels;

    private HealthManager _healthManager;
    
    void Start()
    {
        _healthManager = GetComponent<HealthManager>();
    }
    
    private void FixedUpdate()
    {
        if (currentLevel >= expToLevelUp.Length)
        {
            return;
        }

        if (currentExp >= expToLevelUp[currentLevel])
        {
            currentLevel++;
            _healthManager.UpdateMaxHealth(hPLevels[currentLevel]);
        }
    }

    public void AddExperience(int exp)
    {
        currentExp += exp;
    }
}
