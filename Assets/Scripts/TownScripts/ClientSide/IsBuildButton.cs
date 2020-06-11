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
    [SerializeField] private GameObject m_LoadPanel;
    [SerializeField] private PopUpMenus popMenus;
    [SerializeField] private Animator loader;

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

    //Лоадер
    public void StartLoader()
    {
        popMenus.Open(m_LoadPanel);
        InvokeRepeating("LoadInvoke", 1f, 1f);
    }

    private void LoadInvoke()
    {
        if(CoroutinesMaster.CheckCoroutines())
        {
            popMenus.Close(m_LoadPanel);
            CancelInvoke();
        }
    }
}


