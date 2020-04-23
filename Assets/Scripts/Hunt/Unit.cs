using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Комментарии на английском, потому что задолбался писать на русском - слишком долго и непонятно
public class Unit : MonoBehaviour
{
    /*Поля для движения unit'а*/
    [SerializeField] protected float moveSpeed;


    /*Attack fields*/
    [SerializeField] protected float timeBtwAttack;
    [SerializeField] protected float startTimeBtwAttack = .5f;


    /*Attack range*/
    [SerializeField] protected Transform atackPoint;
    [SerializeField] protected float atackRange = .5f;


    /*Character(Enemy & Player) stats(fields)*/
    [SerializeField]
    protected int
        damage,                 //damage of character
        maxHealth,              //maximum character health 
        currentHealth,          //current helath character
        energy;                 //energy for player

    public bool isDying = false;
}
