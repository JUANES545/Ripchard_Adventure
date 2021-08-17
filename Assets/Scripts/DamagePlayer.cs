<using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] GameObject hurtPlayerAnimation;
    [SerializeField] GameObject PlayerDamageNumber;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Colisión entre enemigo y jugador
            
            CharacterStats stats = other.gameObject.GetComponent<CharacterStats>();
            int totalDamage = damage - stats.defenseLevels[stats.currentLevel];
            if (totalDamage <= 0)
            {
                totalDamage = 1;
            }
            
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            //Animación para mostrar puntos de vida restados por el enemigo
            Transform playerPos = other.gameObject.GetComponent<HealthManager>().transform;
            Instantiate(hurtPlayerAnimation, playerPos.position, playerPos.rotation).transform.SetParent(playerPos);
            
            var clone = (GameObject) Instantiate(PlayerDamageNumber, playerPos.transform.position,
                Quaternion.Euler(Vector3.zero));
            clone.transform.SetParent(playerPos);
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;
        }
    }
}