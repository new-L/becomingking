using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Xml.Linq;

public class Army : Population
{

    public override void Add(int number)
    {
        Count += number;
    }

   public override void Reduce(int number)
    {
       Count -= number;
    }

}
