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
}
