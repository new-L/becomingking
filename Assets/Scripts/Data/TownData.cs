using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownData
{
    //Поле, хранящее положение выбранного места под строительство
    private static short chosenPlace;
    //Поле, хранящее возможность постройки зданий(для кнопки)
    private static bool isBuild;
    //Поле, хранящее код здания
    private static string buildingCode;
    public static short ChosenPlace { get => chosenPlace; set => chosenPlace = value; }
    public static bool IsBuild { get => isBuild; set => isBuild = value; }
    public static string BuildingCode { get => buildingCode; set => buildingCode = value; }
}
