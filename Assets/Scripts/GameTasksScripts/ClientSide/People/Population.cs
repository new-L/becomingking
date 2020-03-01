using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Linq;

public class Population : MonoBehaviour
{
    //Поле общего числа населения
    private  int count;


    public  int Count { get => count; set => count = value; }

    public virtual void Add(int number)
    {
        Count += number;
    }

    public virtual void Reduce(int number)
    {
        Count -= number;
    }
}
