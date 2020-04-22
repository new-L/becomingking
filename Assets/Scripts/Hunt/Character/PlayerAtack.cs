using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : Unit
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform atackPoint;
    [SerializeField] private float atackRange = .5f;

    [SerializeField] private float timeBtwAttack;
    [SerializeField] private float startTimeBtwAttack = .5f;

    public LayerMask wahtIsEnemy;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Atack();
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else timeBtwAttack -= Time.deltaTime;
    }

    private void Atack()
    {
        //Play atack animation
        animator.SetTrigger("Atack");

        //Detect enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atackPoint.position, atackRange, wahtIsEnemy);

        //Damage enemy
        foreach(Collider2D enemy in hitEnemies)
        {
            print("Hit" + enemy.name);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (atackPoint == null) return;
        Gizmos.DrawWireSphere(atackPoint.position, atackRange);
    }
}
