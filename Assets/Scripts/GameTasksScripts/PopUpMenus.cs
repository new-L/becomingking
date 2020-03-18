using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Класс, отвечающий за КОНТРОЛЬ всплывающих меню (меню, окно ресурсов, окно рейтинга, окно заданий)
public class PopUpMenus : MonoBehaviour
{
    [SerializeField]
    private GameObject[] menu;

    void Start()
    {
        for(int i = 0; i < menu.Length;i++) menu[i].SetActive(false);
    }


    public void Open(GameObject type)
    {
        type.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Close();
    }

    public void Close()
    {
        for (int i = 0; i < menu.Length; i++) if (menu[i].activeSelf) menu[i].SetActive(false);
    }



}
