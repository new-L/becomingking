using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] private GameObject alert;
    private void Start()
    {
        AlertOff();
    }
    public void AlertOn()
    {
        alert.SetActive(true);
    }
    public void AlertOff()
    {
        alert.SetActive(false);
    }
}
