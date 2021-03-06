﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatement : Unit, IDamageble
{
    [SerializeField] private Animator animator;

    private PlayerStats stats;
    private PlayerItemsCount itemsCount;

    public LayerMask wahtIsEnemy;

    public UnityEvent HealthBarChange;
    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public float CurrentEnergy { get { return currentEnergy; } set { currentEnergy = value; } }
    public float MaxEnergy { get { return maxEnergy; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        itemsCount = FindObjectOfType<PlayerItemsCount>();
    }
    public void WriteStats()
    {
        maxHealth = GetPlayerStats.playerStats.health;
        currentHealth = maxHealth;
        maxEnergy = GetPlayerStats.playerStats.armor;
        currentEnergy = maxEnergy;
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
            if (Input.GetButtonDown("HealthPotion")) Heal();
            else if (Input.GetButtonDown("EnergyPotion")) Energy();
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
        animator.SetTrigger("Hurt");
        currentHealth -= takenDamage;
        HealthBarChange.Invoke();
        if (currentHealth <= 0) Dying();
    }
    public void Dying()
    {
        isDying = true;
        animator.SetTrigger("Dying");
        DyingTrigger respawn = FindObjectOfType<DyingTrigger>();
        StartCoroutine(respawn.OnEnter());
    }

    private void Heal()
    {
        if(GetItemsCharacter.items.healthpotion > 0)
        {
            GetItemsCharacter.items.healthpotion--;
            stats.AddHealth(maxHealth / 100 * 5);
            itemsCount.SetPotionText();
        }
    }
    private void Energy()
    {
        if (GetItemsCharacter.items.energypotion > 0)
        {
            GetItemsCharacter.items.energypotion--;
            stats.AddEnergy((float)maxEnergy / 100 * 5);
            itemsCount.SetPotionText();
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (atackPoint == null) return;
        Gizmos.DrawWireSphere(atackPoint.position, atackRange);
    }
}
