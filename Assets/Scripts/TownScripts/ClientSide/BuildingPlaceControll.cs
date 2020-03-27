using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlaceControll : MonoBehaviour
{
    [SerializeField] private PopUpMenus menus;
    [SerializeField] private GameObject blurPanel, buildOption;
    [SerializeField] private SetBuildingData setData;

    public void ChosenPlace()
    {
        Regex regex = new Regex(@"[0-9]");
        MatchCollection matches = regex.Matches(gameObject.name);
        foreach (Match match in matches)
        {
            TownData.ChosenPlace = (short)(Convert.ToInt16(match.Value) + 1);
        }
    }


    public void OnClick()
    {
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
        }
        ChosenPlace();
    }


    
    

}
