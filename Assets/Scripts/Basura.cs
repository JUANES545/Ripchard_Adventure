using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private Rigidbody2D npcRigidbody;
    [SerializeField] Collider2D zoneMovement;
    public bool isInZone;
    public bool isWalking;
    public float walkTime = 1.5f;
    public float walkCounter;
    public float waitTime = 3.0f;
    public float waitCounter;
    
    public int currentDirection;
    public int latestDirection;
    public Vector2[] walkingDirection =
    {
        new Vector2(-1,0),
        new Vector2(1,0),
        new Vector2(0,-1),
        new Vector2(0,1)
    };
    
    private Animator Anim;
    public Vector2 lastMovement = Vector2.zero;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string moving = "Walking";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private void Start()
    {
        npcRigidbody = GetComponent<Rigidbody2D>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        Anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            if (zoneMovement != null && !isInZone)
            {
                if (currentDirection == 1 || currentDirection == 3)
                {
                    currentDirection--;
                }
                else
                    currentDirection++;
            }

            npcRigidbody.velocity = walkingDirection[currentDirection] * speed;
            walkCounter -= Time.deltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            npcRigidbody.velocity = Vector2.zero;
            waitCounter -= Time.deltaTime;
            if (waitCounter < 0)
            {
                StartWalking();
            }
        }
        AnimationNPC();
    }

    private void StartWalking()
    {
        while (latestDirection == currentDirection)
        {
            currentDirection = UnityEngine.Random.Range(0, 4);
        }
        isWalking = true;
        latestDirection = currentDirection;
        lastMovement = transform.position;
        walkCounter = walkTime;
    }

    private void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
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
        var position = transform.position;
        Anim.SetFloat(horizontal, position.normalized.x);
        Anim.SetFloat(vertical, position.normalized.y);
        Anim.SetBool(moving, isWalking);
        Anim.SetFloat(lastHorizontal, lastMovement.normalized.x);
        Anim.SetFloat(lastVertical, lastMovement.normalized.y);
        
    }
}

