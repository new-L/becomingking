using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public void BackToAuth()
    {
        SceneManager.LoadScene("Аuthorization");
    }

    public void Start()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 133);
    }

    public void OnPointerEnter()
    {
        gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
    }


    public void OnPointerExit()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 133);
    }
}
