using System;
using System.Collections.Generic;
using UnityEngine;

public class SetBonusInfo : MonoBehaviour
{
    private TimeSpan offlineTime;

    public void OfflineChecker()
    {
        offlineTime = DateTime.Now - TimerData.DateTime;
        TimerData.TotalMinutesOffline = (int)offlineTime.TotalMinutes;
    }
}
