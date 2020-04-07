using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private SetTimerInfo timerInfo;
    [SerializeField] private SendingBonusInfo sendInfo;
    
    private void Start()
    {
        StartCoroutine(sendInfo.SendOfflineCheck("get"));
    }

    private void Update()
    {
        if(TimerData.Activate)
        {
            TimerData.CurrentTime = TimerData.EndTime - DateTime.Now.Minute;
            if(TimerData.CurrentTime < 0)
            {
                TimerData.CurrentTime = 60 - DateTime.Now.Minute;
            }
            timerInfo.SetInfo(TimerData.Activate, TimerData.CurrentTime);
            if (TimerData.CurrentTime == 0)
            {
                TimerData.Activate = false;
                StartCoroutine(sendInfo.SendInfo(false));
                timerInfo.GetBuildingsBonus();
            }
        }
    }

    public void SaveDateOnQuit()
    {
        StartCoroutine(sendInfo.SendOfflineCheck("post"));
    }

    private void OnApplicationQuit()
    {
        SaveDateOnQuit();
    }
}
