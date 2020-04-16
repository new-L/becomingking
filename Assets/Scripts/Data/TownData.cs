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
    //Поле, хранящее ID здания
    private static string buildingID;
    //Поле, хранящее тип выбираемого здания(обычное/главное)
    private static string buildingType;
    public static short ChosenPlace { get => chosenPlace; set => chosenPlace = value; }
    public static bool IsBuild { get => isBuild; set => isBuild = value; }
    public static string BuildingCode { get => buildingCode; set => buildingCode = value; }
    public static string BuildingType { get => buildingType; set => buildingType = value; }
    public static string BuildingID { get => buildingID; set => buildingID = value; }
}
