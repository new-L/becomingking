using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rating : MonoBehaviour
{
    [SerializeField]
    //Рейтинг
    private long playerRating;
    //Порядковый номер игрока по рейтингу
    private int playerSerialNumber;
    public long PlayerRating { get => playerRating; set => playerRating = value; }
    public int PlayerSerialNumber { get => playerSerialNumber; set => playerSerialNumber = value; }

    public void Add(int add)
    {
        playerRating += playerRating;
    }
}
