using System;
using UnityEngine;


public class ArmyTask : Task, ITaskRules
{
    [SerializeField] private CharacterLevel characterLevel;
    [SerializeField] private UpdatePeopleData updatePeople;
    [SerializeField] private int monsterHP;
    [SerializeField] private int armyLosses;
    public void AllProccesses()
    {
        GetActualArmy();
        TimerProccessing();
        MonsterHP();
        SetDiamondChance();
        RewardProccesing();
        ExpProccessing();
        SetTypeReward();
        Losses();
        SetRating();
        TaskData.DiamondCount = 0;        
    }
    public void TimerProccessing()
    {
        Timer = Convert.ToInt32(Math.Round(characterLevel.Level * 0.5d + 10));
        TaskData.Timer = Timer;
    }
    //Жизнь моба (РЕД. Урон игрока считать**)
    private void MonsterHP()
    {
        monsterHP = Convert.ToInt32(Math.Round(100 * characterLevel.Level * 12.3d));
        TaskData.MonstersHP = monsterHP;
    }
    //Расчитываем количество награды
    public void RewardProccesing()
    {
        //Для расчета рандома;
        int random = UnityEngine.Random.Range(1, 100);
        AmountReward = Convert.ToInt32(Math.Round(monsterHP/13.1d) + random);
        TaskData.ResourceReward = AmountReward;
    }
    public void ExpProccessing()
    {
        //Для расчета рандома;
        int random = UnityEngine.Random.Range(1, 10);
        Exp = Convert.ToInt32(Math.Floor((float)AmountReward / (float)random * 2));
        TaskData.Exptotask = Exp;
    }
    //Определяем шанс найти кристаллы для задания
    public void SetDiamondChance()
    {
        DimonChance = 10;
        TaskData.DiamondChance = DimonChance;
    }
    public void SetTypeReward()
    {
        TypeReward = "gold";
        TaskData.TypeResource = TypeReward;
    }
    public void Losses()
    {
        armyLosses = Convert.ToInt32(Math.Floor(updatePeople.army.Count * (10f / 100f)));
        if (armyLosses == 0) armyLosses = 1;
        TaskData.ArmySLosses = armyLosses;
    }
    private void GetActualArmy()
    {
        updatePeople.UpdatePeople();
    }
    private void SetRating()
    {
        TaskData.Rating = characterLevel.Level * 2 + 10;
    }
}
