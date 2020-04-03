using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabBuildingController : MonoBehaviour
{
    [SerializeField] private Text building_code;
    [SerializeField] private SetBuildingData setData;
    [SerializeField] private IsBuildButton buildButton;
    /* Повтор здания */
    [SerializeField] private ReiterativeBuilding reBuilding;
    private void Start()
    {
        setData = GameObject.Find("SceneController").GetComponent<SetBuildingData>();
        reBuilding = GameObject.Find("SceneController").GetComponent<ReiterativeBuilding>();
    }
    private void OnMouseUp()
    {
        setData.BuildingData(building_code.text);
    }

    private void OnMouseEnter()
    {
        setData.BuildingData(building_code.text);
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("buildings_icon/" + building_code.text + "_v1");
        foreach (var model in GetDataBuildings.dataBuildings)
        {
            if(model.code.Equals(building_code.text) && reBuilding.ReiterativeBuildingCheck(model))
            {
                buildButton = GameObject.Find("BuildB").GetComponent<IsBuildButton>();
                buildButton.SetButtonSprite("_build_normal");
            }
        }
    }

    private void OnMouseExit()
    {
        setData.BuildingData(building_code.text);
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("buildings_icon/" + building_code.text);
    }

}
