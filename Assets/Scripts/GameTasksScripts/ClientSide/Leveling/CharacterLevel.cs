using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLevel : MonoBehaviour
{
    //private List<>
    //Переменные уровня и опыта
    private int level, exp;

    public int Level { get { return level; } set { level = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public void AddLevel(int add)
    {
        level += add;
    }
    public void AddExp(int add)
    {
        exp += add;
    }

}
