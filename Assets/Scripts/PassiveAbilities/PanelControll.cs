using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControll : MonoBehaviour
{
    public void OnPointerEnter(GameObject button)
    {
        button.GetComponent<Image>().color = new Color32(255, 255, 255, 158);
    }

    public void OnPointerExit(GameObject button)
    {
        button.GetComponent<Image>().color = new Color32(255, 255, 255, 132);
    }



    public void HuntEnter(GameObject button)
    {
        button.GetComponent<Image>().color = new Color32(133, 133, 133, 255);
    }

    public void HuntExit(GameObject button)
    {
        button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
