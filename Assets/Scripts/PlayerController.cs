using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator Anim;
    public float runningSpeed = 6f;
    private Vector3 _moveDirection;
    private SFXManager _sfxManager;
    
    public Vector2 lastMovement = Vector2.zero;
    public string nextPlaceName;
    
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    private const string speed = "Speed";
    private const string attackingState = "Attacking";

    [SerializeField] float attackTime;
    private float attackTimeCounter;
    public bool attacking = false;
    public static bool playerCreated;

    public bool playerTalking;
    

    private void Start()
    {
        _sfxManager = FindObjectOfType<SFXManager>();
        if (!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(this.transform.gameObject);
            Anim = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
        playerTalking = false;
        lastMovement = new Vector2(0, -1);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (playerTalking)
        {
            return;
        }

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                Anim.SetBool(attackingState, attacking);
            }
        }
        // S = V*t
        transform.position += _moveDirection * (Time.deltaTime * runningSpeed);
        
        Anim.SetFloat(horizontal, _moveDirection.x);
        Anim.SetFloat(vertical, _moveDirection.y);
        
        Anim.SetFloat(lastHorizontal, lastMovement.x);
        Anim.SetFloat(lastVertical, lastMovement.y);
    }
    
    public void Move(InputAction.CallbackContext context){
        _moveDirection = context.ReadValue<Vector2>();
        Anim.SetFloat(speed, _moveDirection.magnitude);
        if (_moveDirection.magnitude > 0.1f)
        {
            lastMovement = new Vector2(_moveDirection.x,_moveDirection.y);
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (playerTalking) return;

        if (!context.performed) return;
        attacking = true;
        attackTimeCounter = attackTime;
        Anim.SetBool(attackingState, attacking);
        _sfxManager.playerAttack.Play();
    }
}
