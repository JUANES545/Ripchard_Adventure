using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2;
    [SerializeField] private float timeToMakeStep;
    [SerializeField] private float timeBetweenSteps;
    
    public Vector2 directionToMakeStep;
    
    public bool isMoving = false;
    public bool playerDetected = false;
    private float timeBetweenStepsCounter;
    private float timeToMakeStepCounter;
    private Rigidbody2D enemyRigidbody;
    private Transform player;
    
    private Vector2 lastMovement = Vector2.zero;
    private Vector2 directionPlayer = Vector2.zero;
    private Animator enemyAnimator;

    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string moving = "Walking";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    private void Awake()
    {
        playerDetected = false;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform; //transform de nuestro player
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        timeBetweenStepsCounter = timeBetweenSteps * UnityEngine.Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * UnityEngine.Random.Range(0.5f, 1.5f);
    }

    void FixedUpdate()
    {
        if (!playerDetected)
        {
            
            if (isMoving)
            {
                timeToMakeStepCounter -= Time.deltaTime;
                enemyRigidbody.velocity = directionToMakeStep;
            
                if (timeToMakeStepCounter < 0)
                {
                    isMoving = false;
                    timeBetweenStepsCounter = timeBetweenSteps;
                    enemyRigidbody.velocity = Vector2.zero;
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.deltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    isMoving = true;
                    timeToMakeStepCounter = timeToMakeStep;
            
                    directionToMakeStep = new Vector2(
                        UnityEngine.Random.Range(-1, 2), // de esta forma porque el Random.Range no llega al ultimo
                        UnityEngine.Random.Range(-1, 2)
                    ) * enemySpeed;
            
                    lastMovement = directionToMakeStep;
                }
            }
        }
        else
        {
            enemyRigidbody.velocity = Vector2.zero;
            directionPlayer = player.position - transform.position;
            transform.position += (Vector3) directionPlayer.normalized * (Time.deltaTime * enemySpeed);
            isMoving = directionPlayer.magnitude > 0.1;
            lastMovement = directionPlayer;
            
        }

        AnimationEnemy();
    }

    private void OnTriggerEnter2D(Collider2D other) //DangerZone detection
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) //DangerZone detection
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    private void AnimationEnemy()
    {
        if (!playerDetected)
        {
            enemyAnimator.SetFloat(horizontal, directionToMakeStep.x);
            enemyAnimator.SetFloat(vertical, directionToMakeStep.y);
        }else
        {
            enemyAnimator.SetFloat(horizontal, directionPlayer.x);
            enemyAnimator.SetFloat(vertical, directionPlayer.y);
        }
        if (lastMovement != Vector2.zero)
        {
            enemyAnimator.SetBool(moving, isMoving);
        }
        enemyAnimator.SetFloat(lastHorizontal, lastMovement.x);
        enemyAnimator.SetFloat(lastVertical, lastMovement.y);
        
    }
}
