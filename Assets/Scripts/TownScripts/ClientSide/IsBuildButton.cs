using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IsBuildButton : MonoBehaviour
{

    public void Highlited()
    {
        SetButtonSprite("_build_highlited");
    }

    public void UnHighlited()
    {
        SetButtonSprite("_build_normal");
    }



    public void SetButtonSprite(string state)
    {
        if (TownData.IsBuild) gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(state);
        else gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("_build_none");
    }

}
