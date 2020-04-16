using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class IntitalizeModelAbilities : MonoBehaviour
{
    /*Здания игрока*/
    [SerializeField] private GetPlayerBuildings getPlayerBuildings;
    /*Контент зданий*/
    [SerializeField] private RectTransform content;
    /*Префаб иконки здания*/
    [SerializeField] private RectTransform prefab;
    //Инициализация префабов зданий
    public void SetAllAbilitiesInfo()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (var model in GetDataBuildings.dataBuildings)
        {
            foreach (var building in getPlayerBuildings.playerBuildings)
            {
                if (model.id == building.building_id && building.building_id != 999)
                {
                    var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
                    instance.transform.SetParent(content, false);
                    InitializeItemView(instance, model);
                }
            }
        }
    }

    //Отрисовка префабов и установка данных
    void InitializeItemView(GameObject viewGameObject, Building building)
    {
        viewGameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("passive_abilities/_passive" + building.code);
    }

}
