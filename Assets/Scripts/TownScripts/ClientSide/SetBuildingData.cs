using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBuildingData : MonoBehaviour
{

    [SerializeField] private Image[] building_place;
    /*Здания игрока*/
    [SerializeField] private GetPlayerBuildings getPlayerBuildings;
    /*Контент зданий*/
    [SerializeField] private RectTransform content;
    /*Префаб иконки здания*/
    [SerializeField] private RectTransform prefab;
    /* Конвертер цифр */
    [SerializeField] private NumberConversion numberConversion;
    /* Количество золота */
    [SerializeField] private Gold gold;
    /* Ресурсы */
    [SerializeField] private GetResourcePlayer getRS;
    //Картинка награды
    [SerializeField] private Image rewardI;
    /* Повтор здания */
    [SerializeField] private ReiterativeBuilding reBuilding;
    public void SetBuildingPlaceInfo()
    {
        int buildingCount = building_place.Length;
        for(int i = 0; i < buildingCount; i++)
        {
            if(getPlayerBuildings.playerBuildings[i].building_id == 999)
            {
                if (building_place[i].tag.Equals("MainBuilding"))
                {
                    building_place[i].sprite = Resources.Load<Sprite>("buildings_icon/_main_building_place");
                }
                else 
                {
                    building_place[i].sprite = Resources.Load<Sprite>("buildings_icon/_building_place");
                }
                building_place[i].GetComponentInChildren<Text>().text = "not builded";
            }
            else
            {
                building_place[i].sprite = Resources.Load<Sprite>("buildings_icon/" + BuildedBuildings(getPlayerBuildings.playerBuildings[i].building_id));
                building_place[i].GetComponentInChildren<Text>().text = getPlayerBuildings.playerBuildings[i].building_id.ToString();
            }
        }
    }

    private string BuildedBuildings(int id)
    {
        string code = "none";
        foreach(var item in GetDataBuildings.dataBuildings)
        {
            if (id == item.id) { code = item.code; break; }
        }
        return code;
    }



 //Инициализация префабов зданий
    public void SetAllBuildingsInfo()
    {
        print(TownData.BuildingType);
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (var model in GetDataBuildings.dataBuildings)
        {
            if (model.code.Contains("_obelisk") && TownData.BuildingType.Equals("obelisk"))
            {
                var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
                instance.transform.SetParent(content, false);
                InitializeItemView(instance, model);
            }
            else if(model.code.Contains("_main") && TownData.BuildingType.Equals("main"))
            {
                var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
                instance.transform.SetParent(content, false);
                InitializeItemView(instance, model);
            }
        }
    }

    //Отрисовка префабов и установка данных
    void InitializeItemView(GameObject viewGameObject, Building building)
    {
        BuildingPrefabComponents view = new BuildingPrefabComponents(viewGameObject.transform);

        view.building_code.text = building.code;
        viewGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("buildings_icon/" + building.code);
    }

    public class BuildingPrefabComponents
    {
        public Text building_code;

        public BuildingPrefabComponents(Transform rootView)
        {
            building_code = rootView.Find("building_code").GetComponent<Text>();
        }
    }
    [SerializeField] private PopUpMenus menus;
    [SerializeField] private GameObject InfoPanel, alertText;
    //ТекстUI для каждого из компонентов постройки здания
    [SerializeField] private Text buildingNameT, buildingDescriptionT, buildingGoldT, buildingWoodT,
                    buildingRockT, buildingLimestoneT, buildingWheatT, buildingRewardT;
    [SerializeField] private int checkBuildNumberAccess;

    private void Start()
    {
        menus.Close(InfoPanel);
        menus.Open(alertText);
    }
    //Общая информация о конкретном здании
    public void BuildingData(string building_code)
    {
        foreach (var model in GetDataBuildings.dataBuildings)
        {
            if (model.code.Equals(building_code))
            {
                menus.Open(InfoPanel);
                menus.Close(alertText);
                checkBuildNumberAccess = 0;
                SetText(model.name, buildingNameT);
                SetText(model.description, buildingDescriptionT);
                SetText(model.costgold, buildingGoldT, "gold");
                SetText(model.costwood, buildingWoodT, "wood");
                SetText(model.costrock, buildingRockT, "rock");
                SetText(model.costlimestone, buildingLimestoneT, "limestone");
                SetText(model.costwheat, buildingWheatT, "wheat");
                SetRewardImage(model.code);
                SetText("+" + model.startbuff.ToString(), buildingRewardT);
                if (reBuilding.ReiterativeBuildingCheck(model)) Attention(true);
                else Attention(false);
                if (checkBuildNumberAccess == 5) TownData.IsBuild = true;
                else { TownData.IsBuild = false; }
                TownData.BuildingCode = model.code.ToString();
                TownData.BuildingID = model.id.ToString();
                break;
            }
        }
    }
    private void SetRewardImage(string sprite)
    {
        rewardI.sprite = Resources.Load<Sprite>("town_reward_icons/_reward" + sprite);
    }
    private void SetText(string model, Text textElement)
    {
        textElement.text = model;
    }

    private void SetText(int model, Text textElement, string resourceName)
    {
        textElement.text = numberConversion.NumberConverter((long)model).ToString();
        if (BuildResources(resourceName, model)) textElement.color = new Color32(236, 77, 77, 255);
        else { textElement.color = new Color32(229, 229, 229, 255); checkBuildNumberAccess++; }
    }

    private void SetText(double model, Text textElement)
    {
        textElement.text = model.ToString();
    }

    private bool BuildResources(string resourceName, int target)
    {
        bool isBuild = true;
        if (!resourceName.Equals("gold"))
        {
            foreach (var resource in getRS.playerResources)
            {
                if (resource.code.Equals(resourceName)) isBuild = (target > resource.count) ? true : false;
                else continue;
            }
        }
        else isBuild = (target > gold.Count) ? true : false;
        
        return isBuild;
    }

    [SerializeField] private GameObject attentionT, buildB;
    private void Attention(bool variable)
    {
        attentionT.SetActive(!variable);
        buildB.SetActive(variable);
    } 
}
