using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntLevelData : MonoBehaviour
{
    private static int coinCount;
    private static int coinChessCount;
    private static int resourceCount;
    public static int CoinCount { get => coinCount; set => coinCount = value; }
    public static int CoinChessCount { get => coinChessCount; set => coinChessCount = value; }
    public static int ResourceCount { get => resourceCount; set => resourceCount = value; }
}
