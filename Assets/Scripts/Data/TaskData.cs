using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskData : MonoBehaviour
{
    //Проверка на выполненное задание
    private static bool isComplete;
    // Тип задания
    private static string typeTask;
    //Рейтинг для заданий
    private static int rating;
    //Таймер для задания
    private static int timer;
    //Запоминаем количество наград, для выполнения  задания
    private static int socialPReward;
    private static int workerPReward;
    private static int armyPReward;
    private static int goldWLosses;
    private static int goldALosses;
    private static int exptotask;
    //Запоминаем количество кликов, необходимое для выполнения задания типа РАБОЧИЙ
    private static int clickCount;
    private static int diamondChance;
    private static string typeResource;
    private static int resourceReward;
    //Запоминаем количество хп у монстра, необходимое для выполнения задания
    private static int monstersHP;
    //Запоминаем потери армии после выполнения задания
    private static int armySLosses;
    //Переменная для выпадающих кристаллов
    private static int diamondCount;

    public static string TypeTask { get => typeTask; set => typeTask = value; }
    public static int GoldWLosses { get => goldWLosses; set => goldWLosses = value; }
    public static int ArmyPReward { get => armyPReward; set => armyPReward = value; }
    public static int WorkerPReward { get => workerPReward; set => workerPReward = value; }
    public static int SocialPReward { get => socialPReward; set => socialPReward = value; }
    public static int GoldALosses { get => goldALosses; set => goldALosses = value; }
    public static int ClickCount { get => clickCount; set => clickCount = value; }
    public static int DiamondChance { get => diamondChance; set => diamondChance = value; }
    public static int Exptotask { get => exptotask; set => exptotask = value; }
    public static int ResourceReward { get => resourceReward; set => resourceReward = value; }
    public static string TypeResource { get => typeResource; set => typeResource = value; }
    public static int MonstersHP { get => monstersHP; set => monstersHP = value; }
    public static int ArmySLosses { get => armySLosses; set => armySLosses = value; }
    public static int DiamondCount { get => diamondCount; set => diamondCount = value; }
    public static int Rating { get => rating; set => rating = value; }
    public static int Timer { get => timer; set => timer = value; }
    public static bool IsComplete { get => isComplete; set => isComplete = value; }
}
