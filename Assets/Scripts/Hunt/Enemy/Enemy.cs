using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private Rigidbody2D enemy_Rigidbody;
    [SerializeField] private Animator enemy_Animator;
    [SerializeField] private GameObject alive, character;
    [SerializeField] private LayerMask whatIsGround;//Слой для детектеда конца платформы или стены
    [SerializeField] private LayerMask characterMask;//Слой для детектеда игрока
    //Проверка расстояний до стены, платфор и игрока
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
    //Поля для проверки того, что сейчас перед врагом(платформа, стена или был заменеч игрок)
    private bool
        groundDetected,
        wallDetected,
        playerDetected;
    
    private int facingDirection;//Направление поворта врага

    public static bool canAttack;//Поле для проверки возможности атаки игрока



    private void Start()
    {
        enemy_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        enemy_Animator = gameObject.GetComponent<Animator>();
        facingDirection = 1;
    }
    private void FixedUpdate()
    {
        UpdateWalkingState();
        UpdateDetectState();
    }

    private void Update()
    {
        if (canAttack) Attack();
    }

    //Состояние Покоя(НЕ IDLE)
    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        if (!groundDetected || wallDetected)
        {
              Flip();
        }
        else if (!playerDetected && !canAttack)
        {
                enemy_Animator.SetInteger("State", 1);
                enemy_Rigidbody.velocity = new Vector2(moveSpeed * facingDirection * Time.fixedDeltaTime, enemy_Rigidbody.velocity.y);
        }
    }

    //Состояние Слежки за персонажем(НЕ IDLE)
    private void UpdateDetectState()
    {
        playerDetected = Physics2D.Raycast(playerCheck.position, transform.right, playerCheckDistance, characterMask);
        if (!playerDetected) playerDetected = Physics2D.Raycast(playerCheck.position, -transform.right, playerCheckDistance + 1f, characterMask);
        if (playerDetected && !canAttack)
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
    }


    

    //Обработчик атаки врага
    private void Attack()
    {
        //Анимация атаки
        enemy_Animator.SetInteger("State", 3);
        //Остановка перед игроком
        enemy_Rigidbody.velocity = new Vector2(0,0);
        
    }
    //Разворот в противоположную стороны
    private void Flip()
    {
        facingDirection *= -1;
        if (facingDirection == -1)
            alive.transform.localRotation = Quaternion.Euler(.0f, -180f, .0f);
        else
            alive.transform.localRotation = Quaternion.Euler(.0f, .0f, .0f);
    }




}