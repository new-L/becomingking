using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAccountDataInfo : MonoBehaviour
{

    [SerializeField]
    private Text goldT, diamondT, levelT, socialT, workerT, armyT;
    /*Валюты*/
    [SerializeField] private Diamond diamond;
    [SerializeField] private Gold gold;
    /*Данные о уровне персонажа*/
    [SerializeField] private CharacterLevel cLevel;
    /*Данные о количестве людей того или иного типа игрока*/
    [SerializeField] private PlayerServerResponse psResponse;
    /*Конвертер чисел*/
    [SerializeField] private NumberConversion numberConversion;


    //Ты пишешь эти строки кода в 2 часа ночи, но знай, эта строка поможет тебе узнать, что делает этот метод. Всё делается во благо!!
    //Проставляем инфу о аккаунте в UI-элементы
    public void SetData()
    {
        //Установка значений валюты игрока
        goldT.text = numberConversion.NumberConverter(gold.Count);
        diamondT.text = numberConversion.NumberConverter(diamond.Count);
        //Установка значений уровня игрока в текстUI
        levelT.text = "Уровень: " + cLevel.Level.ToString();
        //Установка значения количества людей того или иного типа в текстUI
        socialT.text = numberConversion.NumberConverter(psResponse.population.Count);
        armyT.text = numberConversion.NumberConverter(psResponse.army.Count);
        workerT.text = numberConversion.NumberConverter(psResponse.worker.Count);
        // CheckTheTaskAccess();
    }

}
