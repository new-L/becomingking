using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : Unit, IDamageble
{
    [SerializeField] private Animator animator;

    public LayerMask wahtIsEnemy;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void WriteStats()
    {
        maxHealth = GetPlayerStats.playerStats.health;
        currentHealth = maxHealth;
        energy = GetPlayerStats.playerStats.armor;
        damage = GetPlayerStats.playerStats.damage;
    }
    private void Update()
    {
        if(!isDying)
        {
            if (timeBtwAttack <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
            else timeBtwAttack -= Time.deltaTime;  
        }
    }

    public void Attack()
    {
        //Play atack animation
        animator.SetTrigger("Atack");

        //Detect enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atackPoint.position, atackRange, wahtIsEnemy);

        //Damage enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void TakeDamage(int takenDamage)
    {
        currentHealth -= takenDamage;
        if (currentHealth <= 0) Dying();
    }
    public void Dying()
    {
        animator.SetTrigger("Dying");
        isDying = true;
    }


    private void OnDrawGizmosSelected()
    {
        if (atackPoint == null) return;
        Gizmos.DrawWireSphere(atackPoint.position, atackRange);
    }
}
