using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskInfoH : MonoBehaviour
{
    //Поле для определения типа задания
    [SerializeField] private Text taskTypeT;
    //Поле для установки картинки типа задания (социальное, рабочие, армия)
    [SerializeField] private Image tasktypeI;
    //Поле для установки значения таймера перезарядки задания
    [SerializeField] private Text taskTimerT;
    //Поле для установки количества награды людей
    [SerializeField] private Text socialPRewardT;
    //Поле для установки количества награды рабочих
    [SerializeField] private Text workerPRewardT;
    //Поле для установки количества награды армии
    [SerializeField] private Text armyPRewardT;
    //Поле для установки количества потери золота
    [SerializeField] private Text workerLossesT;
    //Поле для установки количества потери золота
    [SerializeField] private Text armyLossesT;
    //Если у нас тип задания ARMY или WORKER нужно сменить картинку основной награды
    [SerializeField] private Image army_worke_reward;
    //UI для потери при выполнении заданий от Армии
    [SerializeField] private Text armyLossesTaskT;
    [SerializeField] private GameObject armyLossesPanel;
    /*Задания*/
    [SerializeField] private SocialTask socialTask;
    [SerializeField] private WorkersTask workersTask;
    [SerializeField] private ArmyTask armyTask;

    [SerializeField] private NumberConversion numberConversion;
    [SerializeField] private Army army;
    [SerializeField] private Workers worker;
    [SerializeField] private GameTasksController gmTaskControll;

    /*Кнопки с заданиями в соц. заданиях*/
    [SerializeField] GameObject option_1, option_2;

    /*Список с типом наград социальных заданий*/
    [SerializeField] private List<string> typeRewardSocial = new List<string>() { "socialS", "workerS", "armyS" };
    public void StartProccessTask()
    {
        switch (TaskData.TypeTask)
        {
            case "social":
                {
                    socialTask.TimerProccessing();
                    SocialProccessing();
                    Check();
                    break;
                }
            case "worker":
                {
                    armyLossesPanel.SetActive(false);
                    workersTask.AllProccesses();
                    SetRewardToArmyWorker();
                    break;
                }
            case "army":
                {
                    armyLossesPanel.SetActive(true);
                    armyTask.AllProccesses();
                    SetRewardToArmyWorker();
                    break;
                }

        }
        SetAllInfo();
    }
    private void SetAllInfo()
    {
        SetType();
        SetTypeTaskImage();
        SetTimer();
        SetRewards();
        SetLosses();
    }
    private void Check()
    {

        if (TaskData.WorkerPReward == 0) { gmTaskControll.ButtonTaskColor(option_1, 121); gmTaskControll.ButtonTaskEnable(option_1, false); }
        else { gmTaskControll.ButtonTaskColor(option_1, 255); gmTaskControll.ButtonTaskEnable(option_1, true); }
        if (TaskData.ArmyPReward == 0) { gmTaskControll.ButtonTaskColor(option_2, 121); gmTaskControll.ButtonTaskEnable(option_2, false); }
        else { gmTaskControll.ButtonTaskColor(option_2, 255); gmTaskControll.ButtonTaskEnable(option_2, true); }

    }
    //Устанавливаем данные о типе задания в текстовое поле, скрытое от глаз пользователя
    private void SetType()
    {
        taskTypeT.text = TaskData.TypeTask;
    }
    //Устанавливаем спрайт-информацию о типе выполняемого задания
    private void SetTypeTaskImage()
    {
        tasktypeI.sprite = Resources.Load<Sprite>("task_type_icon/" + TaskData.TypeTask + "TaskButton");
    }
    
    //Устанавливаем таймер
    private void SetTimer()
    {
        switch (TaskData.TypeTask)
        {
            case "social": taskTimerT.text = socialTask.Timer.ToString(); break;
            case "worker": taskTimerT.text = workersTask.Timer.ToString(); break;
            case "army": taskTimerT.text = armyTask.Timer.ToString(); break;
        }
    }
    //Устанавливаем картинки наград при выполнении заданий типа WORKER или ARMY
    private void SetRewardToArmyWorker()
    {
        switch (TaskData.TypeTask)
        {
            case "worker": army_worke_reward.sprite = Resources.Load<Sprite>("resources_icon/" + TaskData.TypeResource); break;
            case "army": army_worke_reward.sprite = Resources.Load<Sprite>("goldIcon"); break;
        }
    }

    private void SetRewards()
    {
        switch(TaskData.TypeTask)
        {
            case "social":
                {
                    socialPRewardT.text = "+" + numberConversion.NumberConverter(TaskData.SocialPReward);
                    workerPRewardT.text = "+" + numberConversion.NumberConverter(TaskData.WorkerPReward);
                    armyPRewardT.text = "+" + numberConversion.NumberConverter(TaskData.ArmyPReward);
                    break;
                }
        }


    }
    //устанавливаем потери при выполнении заданий
    private void SetLosses()
    {
        switch (TaskData.TypeTask)
        {
            case "social":
            {
                    workerLossesT.text = "-" + numberConversion.NumberConverter(TaskData.GoldWLosses);
                    armyLossesT.text = "-" + numberConversion.NumberConverter(TaskData.GoldALosses);
                    break;
            }
            case "army":
            {
                    armyLossesTaskT.text = "-" + numberConversion.NumberConverter(TaskData.ArmySLosses);
                    break;
            }
        }
    }

    //Устанавливаем данные заданий в TaskData
    private void SocialProccessing()
    {
        foreach (var index in typeRewardSocial)
        {
            socialTask.TypeReward = index;
            socialTask.RewardProccesing();
            socialTask.Losses();
            socialTask.ExpProccessing();
        }
    }

}
