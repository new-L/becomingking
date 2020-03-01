using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    //Поле опыта
    private int exp;
    //Поле таймера
    private int timer;
    //Поле типа награды
    private string typeReward;
    //Поле количества награды
    private int amountReward;
    //Шанс выпадения кристалла на клик
    private int dimonChance;
    public int Exp { get => exp; set => exp = value; }
    public int Timer { get => timer; set => timer = value; }
    public string TypeReward { get => typeReward; set => typeReward = value; }
    public int AmountReward { get => amountReward; set => amountReward = value; }
    public int DimonChance { get => dimonChance; set => dimonChance = value; }
}
