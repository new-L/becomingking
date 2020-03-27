using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiterativeBuilding : MonoBehaviour
{
    [SerializeField] private GetPlayerBuildings playerBuildings;
    public bool ReiterativeBuildingCheck(Building model)
    {
        bool flag = true;
        foreach(var pBuilding in playerBuildings.playerBuildings)
        {
            if (pBuilding.building_id == model.id) return flag = false;
        }
        return flag;
    }
}
