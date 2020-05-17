using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetRateInfo : MonoBehaviour
{
    [SerializeField]
    private Text ratingT, ratingPlaceT, ratingNameT;
    /*Данные о рейтинге игрока*/
    [SerializeField] private Rating rating;
    /*Контент рейтинга*/
    [SerializeField] private RectTransform content;
    /*Префаб мирового рейтинга*/
    [SerializeField] private RectTransform prefab;
    [SerializeField] private Scrollbar scrollbar;
    /*Конвертер чисел*/
    [SerializeField] private NumberConversion numberConversion;
    //Вывод личного рейтинга
    public void SetRate()
    {
        ratingPlaceT.text = rating.PlayerSerialNumber.ToString();
        ratingT.text = numberConversion.NumberConverter(rating.PlayerRating);
        ratingNameT.text = ServerData.GlobalUser;
    }
    //Вывод мирового рейтинга. Позволяет выводить данные, прежде чем сервер ответит на предыдущий метод
    public void SetWorldRate()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        int place = 1;
        foreach (var model in GetRatePlayer.worldRating)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model, place);
            place++;
        }
        //Установка скроллбара на первую позхиицю
        scrollbar.value = 1;
    }


    //Отрисовка префабов и установка данных
    void InitializeItemView(GameObject viewGameObject, WorldRating rating, int place)
    {
        RatingPrefabComponents view = new RatingPrefabComponents(viewGameObject.transform);
        view.place.text = place.ToString();
        view.ratingInfo.text = "Имя: " + rating.name + "\nУровень: " + rating.level.ToString() + "\nРейтинг: " + rating.numberrate.ToString();
    }

    public class RatingPrefabComponents
    {
        public Text place, ratingInfo;

        public RatingPrefabComponents(Transform rootView)
        {
            place = rootView.Find("place").GetComponent<Text>();
            ratingInfo = rootView.Find("ratingInfo").GetComponent<Text>();
        }
    }
}
