using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoadClouds : MonoBehaviour
{
    Animator cloudsAnimator;
    void Start()
    {

        cloudsAnimator = gameObject.GetComponent<Animator>();
        cloudsAnimator.speed = 0.009f;

    }
    private void OnDestroy()
    {
        cloudsAnimator.StopPlayback();
    }
}
