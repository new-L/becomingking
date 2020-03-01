using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITaskRules
{
    //Метод для расчета времени перезарядки
    void TimerProccessing();
    //Методя для расчет опыта на задание
    void ExpProccessing();
    //Методя для расчета награды за задание
    void RewardProccesing();
    //Метод для расчета потерь после выполнения задания
    void Losses();

}
