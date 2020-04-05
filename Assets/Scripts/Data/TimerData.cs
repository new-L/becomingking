using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerData : MonoBehaviour
{
    private static int startTime;
    private static int currentTime;
    private static int endTime;
    private static bool activate;

    public static int StartTime { get => startTime; set => startTime = value; }
    public static int CurrentTime { get => currentTime; set => currentTime = value; }
    public static int EndTime { get => endTime; set => endTime = value; }
    public static bool Activate { get => activate; set => activate = value; }
}
