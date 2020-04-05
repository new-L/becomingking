using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTimerInfo : MonoBehaviour
{
    [SerializeField] private GetPlayerBuildings buildings;

    [SerializeField] private GameObject bonusImage;

    //Узнаем, есть ли у игрока здания, которые дают бонус лишь через некоторое время
    public void GetBuildingsBonus()
    {
        bool flag = false;
        int buffTime = 999;
        foreach(var build in buildings.playerBuildings)
            foreach(var data in GetDataBuildings.dataBuildings)
                if(build.building_id == data.id && data.buffmeasurement.Contains("|"))
                {
                    TimerData.StartTime = DateTime.Now.Minute;
                    flag = true;
                    buffTime = Convert.ToInt32(data.buffmeasurement.Split('|')[1]);
                    if (TimerCheck(buffTime))
                    {
                        TimerData.EndTime = DateTime.Now.Minute + Convert.ToInt32(buffTime);
                        TimerData.CurrentTime = buffTime;
                    }
                    else
                    {
                        TimerData.EndTime = buffTime - (60 - DateTime.Now.Minute);
                        TimerData.CurrentTime = buffTime;
                    }
                }
        SetInfo(flag, TimerData.CurrentTime);
    }
    //Проверяем будет ли возможность просчитать таймер,в  случае, если время перейдет на новый час 
    private bool TimerCheck(int buffTime)
    {
        return DateTime.Now.Minute + buffTime < 60 ? true : false;
    }

    //Устанавливаем значения картинки бонуса, в случае, если есть такие здания
    public void SetInfo(bool active, int currentTime)
    {
        if (active)
        {
            bonusImage.SetActive(true);
            bonusImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("bonusImage_Active");
            bonusImage.GetComponentInChildren<Text>().text = currentTime.ToString() + "м";
            TimerData.Activate = true;
        }
        else
        {
            bonusImage.SetActive(false);
            TimerData.Activate = false;
        }
    }

}
