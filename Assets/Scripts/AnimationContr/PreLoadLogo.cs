using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadLogo : MonoBehaviour
{
    Animator logoAnimator;
    [SerializeField]
    private GameObject loader;
    [SerializeField]
    private PreLoadLoader preLoadLoader;
    void Start()
    {
        logoAnimator = gameObject.GetComponent<Animator>();
        logoAnimator.speed = 0.55f;
        preLoadLoader = GameObject.Find("loader").GetComponent<PreLoadLoader>();
        //Делаем неактивным лоадер при включении
        loader.SetActive(false);
    }
    //Метод, который запускает анимацию лоадера(начало подключения к хостингу)
    void StartLoad()
    {
        loader.SetActive(true);
        preLoadLoader.AnimationActive();
    }
    private void OnDestroy()
    {
        logoAnimator.StopPlayback();
    }
}
