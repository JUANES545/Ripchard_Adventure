using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text moneyText;
    public int currentGold;
    private const string goldKey = "CurrentGold";
    void Start()
    {
        if (PlayerPrefs.HasKey(goldKey))
        {
            currentGold = PlayerPrefs.GetInt(goldKey);
        }
        else
        {
            currentGold = 0;
            PlayerPrefs.SetInt(goldKey, 0);
        }

        moneyText.text = currentGold.ToString();
    }

    public void AddMoney(int moneyCollected)
    {
        currentGold += moneyCollected;
        PlayerPrefs.SetInt(goldKey, currentGold);
        moneyText.text = currentGold.ToString();
    }
}
