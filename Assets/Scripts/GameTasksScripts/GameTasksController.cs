using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTasksController : MonoBehaviour
{
    /*Установка данных для UI элементов задания*/
    [SerializeField] private TaskInfoH taskInfo;
    /*Текста для таймеров*/
    [SerializeField] private Text workerTimer, armyTimer, socialTimer;
    [SerializeField] private GameObject socialButton, workerButton, armyButton;

    //Передача данных при нажатии на какой-либо тип задания
    public void TaskType(string taskType)
    {
        TaskData.TypeTask = taskType;
        taskInfo.StartProccessTask();
    }


    private void Start()
    {
        TaskComplete();
    }
    [SerializeField] private Army army;
    [SerializeField] private Workers worker;
    public void CheckTheTaskAccess()
    {
        if (!IsInvoking("TimerOnArmy"))
        {
            if (army.Count <= 0)
            {
                ButtonTaskColor(armyButton, 121);
                ButtonTaskEnable(armyButton, false);
            }
            else
            {
                ButtonTaskColor(armyButton, 255);
                ButtonTaskEnable(armyButton, true);
            }
        }
        if (!IsInvoking("TimerOnWorker"))
        {
            if (worker.Count <= 0)// && !TaskData.IsComplete)
            {
                ButtonTaskColor(workerButton, 121);
                ButtonTaskEnable(workerButton, false);
            }
            else
            {
                ButtonTaskColor(workerButton, 255);
                ButtonTaskEnable(workerButton, true);
            }
        }
    }
    private int socialTimerSet, workerTimerSet, armyTimerSet;
    public void TaskComplete()
    {
        if(TaskData.IsComplete)
        {            
            switch (TaskData.TypeTask)
            {
                case "social":
                    {
                        SetColorText(socialTimer, 255);
                        ButtonTaskColor(socialButton, 121);
                        ButtonTaskEnable(socialButton, false);
                        socialTimerSet = TaskData.Timer;
                        socialTimer.text = socialTimerSet.ToString();
                        InvokeRepeating("TimerOnSocial", .01f, 1.1f);
                        break;
                    }
                case "worker":
                    {
                        SetColorText(workerTimer, 255);
                        ButtonTaskColor(workerButton, 121);
                        ButtonTaskEnable(workerButton, false);
                        workerTimerSet = TaskData.Timer;
                        workerTimer.text = workerTimerSet.ToString();
                        InvokeRepeating("TimerOnWorker", .01f, 1.1f);
                        break;
                    }
                case "army":
                    {
                        SetColorText(armyTimer, 255);
                        ButtonTaskColor(armyButton, 121);
                        ButtonTaskEnable(armyButton, false);
                        armyTimerSet = TaskData.Timer;
                        armyTimer.text = armyTimerSet.ToString();
                        InvokeRepeating("TimerOnArmy", .01f, 1.1f);
                        break;
                    }
            }
            TaskData.IsComplete = false;
        }
    }
    private void TimerOnSocial()
    {
        socialTimerSet -= 1;
        socialTimer.text = socialTimerSet.ToString();
        if (socialTimerSet == 0)
        {
            CancelInvoke("TimerOnSocial");
            SetColorText(socialTimer, 0);
            ButtonTaskColor(socialButton, 255);
            ButtonTaskEnable(socialButton, true);
        }
    }
    private void TimerOnWorker()
    {
        workerTimerSet -= 1;
        workerTimer.text = workerTimerSet.ToString();
        if (workerTimerSet == 0)
        {
            CancelInvoke("TimerOnWorker");
            SetColorText(workerTimer, 0);
            ButtonTaskColor(workerButton, 255);
            ButtonTaskEnable(workerButton, true);
        }
    }
    private void TimerOnArmy()
    {
        armyTimerSet -= 1;
        armyTimer.text = armyTimerSet.ToString();
        if (armyTimerSet == 0)
        {
            CancelInvoke("TimerOnArmy");
            SetColorText(armyTimer, 0);
            ButtonTaskColor(armyButton, 255);
            ButtonTaskEnable(armyButton, true);
        }
    }
    //Установка RGBA цвета для текстов
    private void SetColorText(Text text, byte opacity)
    {
        text.color = new Color32(255, 255, 255, opacity);
    }
    public void ButtonTaskColor(GameObject imgGO, byte color)
    {
        Image img = imgGO.GetComponent<Image>();
        img.color = new Color32(color,color,color,255);
    }
    public void ButtonTaskEnable(GameObject btnGO, bool isEnable)
    {
        Button btn = btnGO.GetComponent<Button>();
        btn.enabled = isEnable;
    }
}