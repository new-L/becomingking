using System;
using UnityEngine;

public class SocialTask : Task, ITaskRules
{
    [SerializeField] private CharacterLevel characterLevel;
    [SerializeField] private Population population;
    [SerializeField] private int goldLosses;
    public void TimerProccessing()
    {
        Timer = Convert.ToInt32(Math.Round(characterLevel.Level * 0.5d + 10));
        TaskData.Timer = Timer;
    }

    public void ExpProccessing() 
    {
        Exp = 0;
        TaskData.Exptotask = Exp;
    }

    public void RewardProccesing()
    {
        //Для расчета рандома;
        int random = UnityEngine.Random.Range(1, 5);
        switch (TypeReward)
        {
            case "socialS": AmountReward = characterLevel.Level * 2 + random; TaskData.SocialPReward = AmountReward; break;
            case "workerS": AmountReward = Convert.ToInt32(Math.Round(population.Count * 0.13d)); TaskData.WorkerPReward = AmountReward; break;
            case "armyS":  AmountReward = Convert.ToInt32(Math.Round(population.Count * 0.13d)); TaskData.ArmyPReward = AmountReward; break;
        }
        
    }
    
    public void Losses()
    {
        switch (TypeReward)
        {
            case "workerS": goldLosses = Convert.ToInt32(Math.Round(AmountReward * 1.3d)); TaskData.GoldWLosses = goldLosses; break;
            case "armyS": goldLosses = Convert.ToInt32(Math.Round(AmountReward * 1.3d)); TaskData.GoldALosses = goldLosses; break;
        }
        
    }
}
