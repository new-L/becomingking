using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoControll : MonoBehaviour
{
    private int toggle = 0;
    //Скрипт, овтечающий за открытие/закрытие панелей
    [SerializeField] private PopUpMenus menus;
    //Панель грейда, отвечающий за информацию грейда
    [SerializeField] private GameObject upgradePanel;
    //**Здания игрока**//
    [SerializeField] private GetPlayerBuildings playerBuildings;
    //**Текста в панели грейда**//
    [SerializeField] private Text goldT, woodT, wheatT, limestoneT, rockT, rewardT;
    [SerializeField] private Image rewardI;
    //**Просчет данных**//
    private float gold, wood, wheat, limestone, rock, reward, multiple;
    [SerializeField] private NumberConversion conversion;
    //Получаем ресурсы игрока
    [SerializeField] private GetResourcePlayer playerResources;
    //Получаем валюты игрока
    [SerializeField] private Gold playerGold;
    public void OnPointer()
    {
            goldT.text = "-" + conversion.NumberConverter((long)gold);
            woodT.text = "-" + conversion.NumberConverter((long)wood);
            wheatT.text = "-" + conversion.NumberConverter((long)wheat);
            limestoneT.text = "-" + conversion.NumberConverter((long)limestone);
            rockT.text = "-" + conversion.NumberConverter((long)rock);
            rewardT.text = reward.ToString() + "(+" + multiple.ToString() + ")";
            rewardI.sprite = Resources.Load<Sprite>("town_reward_icons/_reward_obelisk_" + TownData.BuildingCode);
            menus.Open(upgradePanel);
    }
    public void UnPointer()
    {
        menus.Close(upgradePanel);
    }
    public void GradeDataH()
    {
        foreach(var item in GetDataBuildings.dataBuildings)
        {
            if(item.code.Equals("_obelisk_" + TownData.BuildingCode))
            {
                gold = item.costgold;
                wood = item.costwood;
                wheat = item.costwheat;
                limestone = item.costlimestone;
                rock = item.costrock;
                reward = item.startbuff;
                reward = item.startbuff;
                foreach (var playerB in playerBuildings.playerBuildings) 
                {
                    if (playerB.building_id == Convert.ToInt32(TownData.BuildingCode))
                    {
                        if (playerB.buildinglvl > 1)
                        {
                            for (int i = 0; i < playerB.buildinglvl - 1; i++)
                            {
                                gold = (float)Math.Round(gold * item.multiplerank, 0, MidpointRounding.AwayFromZero);
                                wood = (float)Math.Round(wood * item.multiplerank, 0);
                                wheat = (float)Math.Round(wheat * item.multiplerank, 0);
                                limestone = (float)Math.Round(limestone * item.multiplerank, 0);
                                rock = (float)Math.Round(rock * item.multiplerank, 0);
                                reward = (float)Math.Round(reward + item.uplvlbuff, 1);
                            }
                        }
                        break;
                    }
                }
                multiple = (float)Math.Round(item.uplvlbuff, 1);
                break;
            }
        }
    }

    public bool GradeOportCheck()
    {
        bool[] flag = new bool[5];
        bool returned = true;
        foreach (var item in playerResources.playerResources)
        {
            switch(item.name)
            {
                case "wood": flag[0] = item.count >= wood ? true : false; break;
                case "rock": flag[1] = item.count >= rock ? true : false; break;
                case "limestone": flag[2] = item.count >= limestone ? true : false; break;
                case "wheat": flag[3] = item.count >= wheat ? true : false; break;
            }
        }
        flag[4] = playerGold.Count >= gold ? true : false;
        foreach(var item in flag)
        {
            if (!item) returned = false;
        }
        return returned;
    }


}
