using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit, IDamageble
{
    [SerializeField] private Rigidbody2D enemy_Rigidbody;
    [SerializeField] private Animator enemy_Animator;
    [SerializeField] private GameObject alive, character;
    [SerializeField] private LayerMask whatIsGround;//Слой для детектеда конца платформы или стены
    [SerializeField] private LayerMask characterMask;//Слой для детектеда игрока
    [SerializeField] private PlayerAtack thePLayer;

    private Collider2D hitCharacterCheck;
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

    public bool canAttack;//Поле для проверки возможности атаки игрока

    private void Awake()
    {
        enemy_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        enemy_Animator = gameObject.GetComponent<Animator>();
        character = GameObject.FindGameObjectWithTag("Character");
        thePLayer = character.GetComponent<PlayerAtack>();
    }

    private void Start()
    {
        facingDirection = 1;
        timeBtwAttack = 2f;
    }
    private void FixedUpdate()
    {
        if(!isDying)
        {
            UpdateWalkingState();
            UpdateDetectState();
        }
    }

    private void Update()
    {
        if (!thePLayer.isDying && !isDying)
        {
            if (canAttack && timeBtwAttack <= 0)
            {
                Attack();
                timeBtwAttack = startTimeBtwAttack + Random.Range(.5f, 1f);
            }
            else
            {

                timeBtwAttack -= Time.deltaTime;
            }
        }
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
        else
        {
            enemy_Animator.SetInteger("State", 0);
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
    public void Attack()
    {
        //Анимация атаки
        enemy_Animator.SetTrigger("Atack");
        //Остановка перед игроком
        enemy_Rigidbody.velocity = new Vector2(0,0);
        //Detect player
        hitCharacterCheck = Physics2D.OverlapCircle(atackPoint.position, atackRange, characterMask);

        //Damage enemy
        if(hitCharacterCheck)
        {
            if(hitCharacterCheck.tag.Equals(character.tag))
            {
                thePLayer.TakeDamage(80);
            }
        }
    }


    //Обработчик получения урона врага
    public void TakeDamage(int takenDamage)
    {
        currentHealth -= takenDamage;
        if (currentHealth <= 0) Dying();
    }

    public void Dying()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        isDying = true;
        enemy_Animator.SetTrigger("Dying");
        Destroy(gameObject, 10f);
    }


    public void WriteStat()
    {
        maxHealth = GetPlayerStats.playerStats.level * 132 + Random.Range(10,100);
        currentHealth = maxHealth;
        damage = GetPlayerStats.playerStats.level * 10 + Random.Range(1, 12);
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