using System;
using UnityEngine;

public class Diamond : MonoBehaviour, ICurrency
{   
    [SerializeField]
    //Количество кристаллов
    private long _count;
    public long Count {get { return _count; } set {_count = value;} }

    //Метод для прибавления количества
    public void Add(int count)
    {
        _count += count;
    }
    //Метод для вычитания количества
    public void Reduce(int count)
    { 
        _count -= count;
    }
}
