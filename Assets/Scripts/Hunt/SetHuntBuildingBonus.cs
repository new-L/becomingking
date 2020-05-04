using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHuntBuildingBonus : MonoBehaviour
{
    public GetPlayerBuildings playerBuildings;
    public void SetBonusBuildingHunt()
    {
        HuntLevelData.ObeliskPrecent = 0;
        foreach (var buildings in playerBuildings.playerBuildings)
        {
            foreach (var item in GetDataBuildings.dataBuildings)
            {
                if (item.id == buildings.building_id && item.code.Equals("_obelisk_2"))
                {
                    if (buildings.buildinglvl > 1)
                    {
                        HuntLevelData.ObeliskPrecent = item.startbuff + (buildings.buildinglvl * item.uplvlbuff);
                    }
                    else if(buildings.buildinglvl == 1)
                    {
                        HuntLevelData.ObeliskPrecent = item.startbuff;
                    }
                }
            }
        }
    }
}
