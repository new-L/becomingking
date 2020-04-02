using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingOption : MonoBehaviour
{
    [SerializeField] Build build;
    [SerializeField] PopUpMenus menu;
    [SerializeField] GameObject buildinOption;
    public void Destroy(Button buildinOptionB)
    {
        buildinOptionB.enabled = false;
        StartCoroutine(build.SendResponse("destroy", buildinOptionB));
        menu.Close();
    }

    public void Upgrade(Button buildinOptionB)
    {
        buildinOptionB.enabled = false;
        StartCoroutine(build.SendResponse("upgrade", buildinOptionB));
        menu.Close();
    }
}
