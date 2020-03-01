using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertAnimation : MonoBehaviour
{
    [SerializeField]
    private Animation animation;
    [SerializeField]
    private Text alertText;
    private void Start()
    {
        animation = gameObject.GetComponent<Animation>();
        animation.Stop();
    }
    public void AnimationActivate(string error)
    {
        alertText.text = error;
        animation.Play();
    }

    public void OnDestroy()
    {
        animation.Stop();
    }


}
