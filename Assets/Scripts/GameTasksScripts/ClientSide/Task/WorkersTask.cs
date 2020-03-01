using System;
using UnityEngine;

public class WorkersTask : Task, ITaskRules
{
    [SerializeField] private CharacterLevel characterLevel;
    //Для определения точного количества ресурса на данный момент
    [SerializeField] private GetResourcePlayer getResource;
    [SerializeField] private UpdatePeopleData updatePeople;
    public void AllProccesses()
    {
        GetActualWorkers();
        TimerProccessing();        
        SetDiamondChance();
        RewardProccesing();
        SetClicks();
        ExpProccessing();
        SetTypeReward();
        SetRating();
        TaskData.DiamondCount = 0;
    }
    public void TimerProccessing()
    { 
        Timer = Convert.ToInt32(Math.Round(characterLevel.Level * 0.5d + 10));
        TaskData.Timer = Timer;
    }
    //Расчитываем количество награды
    public void RewardProccesing()
    {
        AmountReward = Convert.ToInt32(Math.Round(characterLevel.Level * 2 + updatePeople.worker.Count * 1.2d));
        TaskData.ResourceReward = AmountReward;
    }
    //Расчитываем количество кликов, необходимое для выполнения заданяия.
    public void SetClicks()
    {
        TaskData.ClickCount = AmountReward + characterLevel.Level * 2 + 10;
    }
    public void ExpProccessing() 
    {
        Exp = Convert.ToInt32(Math.Round(AmountReward * 1.7d));
        TaskData.Exptotask = Exp;
    }
    //Определяем шанс найти кристаллы для задания
    public void SetDiamondChance()
    {
        DimonChance = 2;
        TaskData.DiamondChance = DimonChance;
    }
    public void SetTypeReward()
    {
        int lengthArrayRes = UnityEngine.Random.Range(0, getResource.playerResources.Length);
        print(lengthArrayRes);
        TypeReward = getResource.playerResources[lengthArrayRes].code;
        TaskData.TypeResource = TypeReward;
    }
    private void GetActualWorkers()
    {
        updatePeople.UpdatePeople();
    }
    private void SetRating()
    {
        TaskData.Rating = characterLevel.Level * 2 + 10;
    }
    public void Losses() { }
}
