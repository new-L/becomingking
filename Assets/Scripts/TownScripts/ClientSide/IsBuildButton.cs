using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IsBuildButton : MonoBehaviour
{
    [SerializeField] private Build build;

    public void Highlited()
    {
        SetButtonSprite("_build_highlited");
    }

    public void UnHighlited()
    {
        SetButtonSprite("_build_normal");
    }


    //Установка спрайта для кнопки
    public void SetButtonSprite(string state)
    {
        if (TownData.IsBuild) gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(state);
        else gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("_build_none");
    }



    //Обработка нажатия кнопки(//**ПОСТРОИТЬ**//)

    public void PostBuildings(Button buildB)
    {
        if (TownData.IsBuild)
        {
            buildB.enabled = false;
            StartCoroutine(build.SendResponse("build", buildB));
        }
    }

}


