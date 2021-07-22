using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float npcSpeed = 1.5f;
    private Rigidbody2D npcRigidbody;
    [SerializeField] Collider2D zoneMovement;
    public bool isInZone;
    public bool isWalking, isTalking;
    public float walkTime = 1.5f;
    public float waitTime = 3.0f;

    public Vector2 directionToMakeStep;
    [SerializeField] private float timeToMakeStep = 1.5f;
    [SerializeField] private float timeBetweenSteps = 3f;
    private float timeBetweenStepsCounter;
    private float timeToMakeStepCounter;
    
    public int currentDirection;

    private Animator Anim;
    private DialogManager _manager;
    
    public Vector2 lastMovement = Vector2.zero;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string moving = "Walking";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private void Start()
    {
        _manager = FindObjectOfType<DialogManager>();
        npcRigidbody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        
        timeBetweenStepsCounter = timeBetweenSteps * UnityEngine.Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * UnityEngine.Random.Range(0.5f, 1.5f);
    }

    private void FixedUpdate()
    {
        if (!_manager.dialogActive)
        {
            isTalking = false;
        }
        if (isTalking)
        {
            StopWalking();
            return;
        }
        if (isWalking)
        {
            if (zoneMovement != null && !isInZone)
            {
                directionToMakeStep = -lastMovement;
                lastMovement = directionToMakeStep;
            }
            
            timeToMakeStepCounter -= Time.deltaTime;
            npcRigidbody.velocity = directionToMakeStep;
            if (timeToMakeStepCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;
            if (timeBetweenStepsCounter < 0)
            {
                StartWalking();
            }
        }
        AnimationNPC();
    }

    private void StartWalking()
    {
        directionToMakeStep = new Vector2(
            UnityEngine.Random.Range(-1, 2),
            UnityEngine.Random.Range(-1, 2)
        ) * npcSpeed;
        isWalking = true;
        lastMovement = directionToMakeStep;
        timeToMakeStepCounter = timeToMakeStep;
    }

    private void StopWalking()
    {
        isWalking = false;
        timeBetweenStepsCounter = timeBetweenSteps;
        npcRigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("MovementZone")) return;
        isInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("MovementZone")) return;
        isInZone = false;
    }
    
    private void AnimationNPC()
    {
        Anim.SetFloat(horizontal, directionToMakeStep.x);
        Anim.SetFloat(vertical, directionToMakeStep.y);
        if (lastMovement != Vector2.zero)
        {
            Anim.SetBool(moving, isWalking);
        }
        Anim.SetFloat(lastHorizontal, lastMovement.x);
        Anim.SetFloat(lastVertical, lastMovement.y);
    }
}

