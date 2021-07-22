using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;
    [SerializeField] GameObject hurtEnemyAnimation;
    [SerializeField] GameObject EnemyDamageNumber;

    private CharacterStats _stats;

    private void Start()
    {
        //_stats = GetComponent<CharacterStats>();
        _stats = GetComponentInParent<CharacterStats>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            int totalDamage = damage;
            if (_stats != null)
            {
                totalDamage += _stats.strengthLevels[_stats.currentLevel];
            }
            
            other.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            Transform enemyPos = other.gameObject.GetComponent<HealthManager>().transform;
            Instantiate(hurtEnemyAnimation, enemyPos.position, enemyPos.rotation).transform.SetParent(enemyPos);

            var clone = (GameObject) Instantiate(EnemyDamageNumber, enemyPos.transform.position,
                Quaternion.Euler(Vector3.zero));
            clone.transform.SetParent(enemyPos);
            clone.GetComponent<DamageNumber>().damagePoints = totalDamage;
        }
    }
}
