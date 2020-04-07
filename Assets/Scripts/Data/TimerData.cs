using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerData : MonoBehaviour
{
    private static int startTime;
    private static int currentTime;
    private static int endTime;
    private static bool activate;
    private static DateTime dateTime;
    private static int totalMinutesOffline;

    public static int StartTime { get => startTime; set => startTime = value; }
    public static int CurrentTime { get => currentTime; set => currentTime = value; }
    public static int EndTime { get => endTime; set => endTime = value; }
    public static bool Activate { get => activate; set => activate = value; }
    public static DateTime DateTime { get => dateTime; set => dateTime = value; }
    public static int TotalMinutesOffline { get => totalMinutesOffline; set => totalMinutesOffline = value; }
}
