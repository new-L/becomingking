using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResourceInfo : MonoBehaviour
{
    /*Контент ресурсов*/
    [SerializeField] private RectTransform resourceContent;
    /*Префаб ресурсов персонажа*/
    [SerializeField] private RectTransform resPrefab;
    /*Ресуры игрока*/
    [SerializeField] private GetResourcePlayer getResource;
    /*Конвертер чисел*/
    [SerializeField] private NumberConversion numberConversion;



    /*Вывод данных о ресурсах персонажа*/
    public void SetResources()
    {
        foreach (Transform child in resourceContent)
        {
            Destroy(child.gameObject);
        }
        foreach (var model in getResource.playerResources)
        {
            var instance = GameObject.Instantiate(resPrefab.gameObject) as GameObject;
            instance.transform.SetParent(resourceContent, false);
            InitializeResourceItemView(instance, model);
        }
    }
    void InitializeResourceItemView(GameObject viewGameObject, PlayerResources playerResources)
    {
        ResourcePrefabComponents view = new ResourcePrefabComponents(viewGameObject.transform);
        view.resName.text = playerResources.name.ToUpperInvariant();
        view.count.text = numberConversion.NumberConverter(playerResources.count);
        view.resCode.text = playerResources.code;
        view.resImage.sprite = Resources.Load<Sprite>("resources_icon/" + playerResources.code);
    }

    public class ResourcePrefabComponents
    {
        public Text resName, count, resCode;
        public Image resImage;

        public ResourcePrefabComponents(Transform rootView)
        {
            resName = rootView.Find("resName").GetComponent<Text>();
            count = rootView.Find("Count").GetComponent<Text>();
            resCode = rootView.Find("resCode").GetComponent<Text>();
            resImage = rootView.Find("resImage").GetComponent<Image>();
        }
    }
}
