using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    private CoinManager _coinManager;

    private void Start()
    {
        _coinManager = FindObjectOfType<CoinManager>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _coinManager.AddMoney(value);
        Destroy(gameObject);
    }
}
