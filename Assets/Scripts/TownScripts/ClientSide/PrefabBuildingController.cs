using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabBuildingController : MonoBehaviour
{
    [SerializeField] private Text building_code;
    [SerializeField] SetBuildingData setData;
    [SerializeField] IsBuildButton buildButton;
    private void Start()
    {
        setData = GameObject.Find("SceneController").GetComponent<SetBuildingData>();
        buildButton = GameObject.Find("BuildB").GetComponent<IsBuildButton>();
    }
    private void OnMouseUp()
    {
        setData.BuildingData(building_code.text);
    }

    private void OnMouseEnter()
    {
        setData.BuildingData(building_code.text);
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("buildings_icon/" + building_code.text + "_v1");
        buildButton.SetButtonSprite("_build_normal");
    }

    private void OnMouseExit()
    {
        setData.BuildingData(building_code.text);
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("buildings_icon/" + building_code.text);
    }

}
