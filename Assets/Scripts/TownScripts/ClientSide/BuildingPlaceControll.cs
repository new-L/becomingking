using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class BuildingPlaceControll : MonoBehaviour
{
    public void ChosenPlace()
    {
        Regex regex = new Regex(@"[0-9]");
        MatchCollection matches = regex.Matches(gameObject.name);
        foreach (Match match in matches)
        {
            TownData.ChosenPlace = (short)(Convert.ToInt16(match.Value) + 1);
        }
        
    }
}
