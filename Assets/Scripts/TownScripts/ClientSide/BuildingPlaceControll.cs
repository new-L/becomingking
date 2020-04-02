using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlaceControll : MonoBehaviour
{
    [SerializeField] private PopUpMenus menus;
    [SerializeField] private GameObject blurPanel, buildOption, blockedImage;
    [SerializeField] private SetBuildingData setData;
    [SerializeField] private UpgradeInfoControll upgradeInfo;
    [SerializeField] private Button upgradeB;

    public void ChosenPlace()
    {
        if (TownData.BuildingType.ToLower().Equals("main"))
        {
            TownData.ChosenPlace = 6;
        }
        else
        {
            Regex regex = new Regex(@"[0-9]");
            MatchCollection matches = regex.Matches(gameObject.name);
            foreach (Match match in matches)
            {
                TownData.ChosenPlace = (short)(Convert.ToInt16(match.Value) + 1);
            }
        }
    }


    public void OnClick(string type)
    {
        TownData.BuildingType = type;
        if (gameObject.GetComponentInChildren<Text>().text.Equals("not builded"))
        {
            menus.Close();
            menus.Open(blurPanel);
            setData.SetAllBuildingsInfo();
        } 
        else
        {
            menus.Open(buildOption);
            TownData.BuildingCode = gameObject.GetComponentInChildren<Text>().text;
            upgradeInfo.GradeDataH();
            if (upgradeInfo.GradeOportCheck())
            {
                upgradeB.enabled = true;
                blockedImage.SetActive(false);
            }
            else
            {
                upgradeB.enabled = false;
                blockedImage.SetActive(true);
            }
        }
        ChosenPlace();
    }


    
    

}
