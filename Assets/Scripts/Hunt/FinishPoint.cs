using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public static List<bool> goalCheck = new List<bool>();
    public static bool goldKey, silverKey, allMonsters;

    [SerializeField] private GameObject m_Borders, m_Win;
    
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private NumberConversion converter;
    private void Start()
    {
        winPanel = FindObjectOfType<WinPanel>();
        m_Win.SetActive(false);
        m_Borders.SetActive(true);
        goalCheck.Clear();
    }

    public void Finish()
    {
        print(goalCheck.Count);
        bool flag = true;
        if (goalCheck.Count == 2)
        {
            foreach (var item in goalCheck)
            {
                print(item);
                if (!item) flag = false;
            }
            if (flag) m_Borders.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Character"))
        {
            Time.timeScale = 0;
            m_Win.SetActive(true);
            SetWinInfo();
        }
    }

    private void SetWinInfo()
    {
        winPanel.SetText(winPanel._goldTotal,converter.NumberConverter(2000), "gold");
        winPanel.SetText(winPanel._goldCoin, converter.NumberConverter(HuntLevelData.CoinCount * 10 * GetPlayerStats.playerStats.level).ToString(), "gold");
        winPanel.SetText(winPanel._goldChess, converter.NumberConverter(HuntLevelData.CoinChessCount * 500 * (GetPlayerStats.playerStats.level / 2)).ToString(), "gold");
        winPanel.SetText(winPanel._resourceItem, converter.NumberConverter(HuntLevelData.ResourceCount * GetPlayerStats.playerStats.level).ToString(), "resources");
        winPanel.SetText(winPanel._resourceTotal, converter.NumberConverter(2000).ToString(), "resources");
    }
}
