using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadLoader : MonoBehaviour
{
    [SerializeField]
    Animator loaderAnimator;
    [SerializeField]
    DBManager dbManager;
    void Awake()
    {
        loaderAnimator = gameObject.GetComponent<Animator>();
        dbManager = GameObject.Find("ServerConnection").GetComponent<DBManager>();
    }

    public void AnimationActive()
    {
        loaderAnimator.Play("loader");
        dbManager.Response(false);
        loaderAnimator.speed = 0.35f;
    }
    private void OnDestroy()
    {
        loaderAnimator.StopPlayback();
    }


}
