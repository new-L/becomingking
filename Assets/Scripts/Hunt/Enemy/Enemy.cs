using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private Rigidbody2D enemy_Rigidbody;
    [SerializeField] private Animator enemy_Animator;
    //Проверка расстояний до стены и платформ
    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        playerCheckDistance;
    [SerializeField]
    private Transform
        groundCheck,
        wallCheck,
        playerCheck;
    //Поля для проверки того, что сейчас перед врагом(платформа или стена)
    private bool
        groundDetected,
        wallDetected,
        playerDetected;
    [SerializeField] private LayerMask whatIsGround;//Слой для детектеда
    [SerializeField] private LayerMask characterMask;
    private int facingDirection;
    public GameObject alive, character;
    
    private void Start()
    {
        enemy_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        enemy_Animator = gameObject.GetComponent<Animator>();
        facingDirection = 1;
    }
    private void FixedUpdate()
    {
        UpdateWalkingState();
    }
    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        playerDetected = Physics2D.Raycast(playerCheck.position, transform.right, playerCheckDistance, characterMask);
        if(!playerDetected) playerDetected = Physics2D.Raycast(playerCheck.position, -transform.right, playerCheckDistance + 2f, characterMask);
        if (!groundDetected || wallDetected)
        {
              Flip();
        }
        else if(playerDetected)
        {
            enemy_Animator.SetInteger("State", 2);
            if (character.transform.position.x > transform.position.x)
            {
                facingDirection = -1;
                Flip();
                enemy_Rigidbody.velocity = new Vector2(moveSpeed * 1.5f * Time.fixedDeltaTime, enemy_Rigidbody.velocity.y);
            }
            else
            {
                facingDirection = 1;
                Flip();
                enemy_Rigidbody.velocity = new Vector2(moveSpeed * -1.5f * Time.fixedDeltaTime, enemy_Rigidbody.velocity.y);
            }
        }
        else
        {
                enemy_Animator.SetInteger("State", 1);
                print(facingDirection);
                enemy_Rigidbody.velocity = new Vector2(moveSpeed * facingDirection * Time.fixedDeltaTime, enemy_Rigidbody.velocity.y);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        if (facingDirection == -1)
            alive.transform.localRotation = Quaternion.Euler(.0f, -180f, .0f);
        else
            alive.transform.localRotation = Quaternion.Euler(.0f, .0f, .0f);
    }




}