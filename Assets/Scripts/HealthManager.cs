using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    [SerializeField] int ExpWhenDefeated;
    
    [Header("IFrame Stuff")] 
    [SerializeField] private Color flashColor;
    [SerializeField] private Color regularColor;
    [SerializeField] private float flashDuration = 0.07f;
    [SerializeField] private int numberOfFlashes = 4;
    private Collider2D _characterTriggerCollider;
    private SpriteRenderer _characterRenderer;
    private GameObject _gameObject;

    public string enemyName;
    private QuestManager _questManager;
    private SFXManager _sfxManager;

    void Start()
    {
        _gameObject = GameObject.Find("Player");
        currentHealth = maxHealth; //iniciamos el juego con la vida maxima.
        _characterRenderer = GetComponent<SpriteRenderer>(); //Obtiene el SpriteRenderer del personaje impactado
        _characterTriggerCollider = GetComponent<Collider2D>();//Obtiene el collider del personaje impactado,
        _questManager = FindObjectOfType<QuestManager>();
        _sfxManager = FindObjectOfType<SFXManager>();
    }
    
    void FixedUpdate()
    {
        
        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                _questManager.enemyKilled = enemyName;
                _gameObject.GetComponent<CharacterStats>().AddExperience(ExpWhenDefeated);
            }

            if (gameObject.CompareTag("Player"))
            {
                _sfxManager.playerDead.Play();
            }
            gameObject.SetActive(false); //Desactivamos el GameObject porque no tiene mas vidas.
        }
    }

    
    public void DamageCharacter(int damage)
    {
        currentHealth -= damage;
        if (gameObject.CompareTag("Player"))
        {
            _sfxManager.playerHurt.Play();
        }

        StartCoroutine(FlashCo());
    }

    public void UpdateMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }
    
    private IEnumerator FlashCo()
    {
        int temp = 0;
        _characterTriggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            _characterRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            _characterRenderer.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp ++;
        }
        _characterTriggerCollider.enabled = true;
    }
}
