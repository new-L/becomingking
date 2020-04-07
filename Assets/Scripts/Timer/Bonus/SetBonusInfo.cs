using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBonusInfo : MonoBehaviour
{
    private TimeSpan offlineTime;
    /*Контент бонусов*/
    [SerializeField] private RectTransform content;
    /*Префаб бонуса*/
    [SerializeField] private RectTransform prefab;
    [SerializeField] private NumberConversion conversion;

    /**/
    [SerializeField] private PopUpMenus menus;
    /**/
    [SerializeField] private GameObject bonuspanel;

    public void OfflineChecker()
    {
        offlineTime = DateTime.Now - TimerData.DateTime;
        TimerData.TotalMinutesOffline = (int)offlineTime.TotalMinutes;
    }


    //Вывод мирового рейтинга. Позволяет выводить данные, прежде чем сервер ответит на предыдущий метод
    public void SetBonus()
    {
        menus.Open(bonuspanel);
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (var model in SendingBonusInfo.bonuses)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model);
        }
    }


    //Отрисовка префабов и установка данных
    void InitializeItemView(GameObject viewGameObject, OfflineBonus model)
    {
        BonusPrefabComponents view = new BonusPrefabComponents(viewGameObject.transform);
        view.bonusImage.sprite = Resources.Load<Sprite>("bonus/_bonus_" + model.type);
        view.bonusCount.text = conversion.NumberConverter(model.bonus).ToString();

    }

    public class BonusPrefabComponents
    {
        public Text bonusCount;
        public Image bonusImage;

        public BonusPrefabComponents(Transform rootView)
        {
            bonusCount = rootView.Find("bonusCount").GetComponent<Text>();
            bonusImage = rootView.Find("bonusImage").GetComponent<Image>();
        }
    }

}
