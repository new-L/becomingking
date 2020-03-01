using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrCreate : MonoBehaviour
{

    [SerializeField]
    private Animator createBtnAnim;
    [SerializeField]
    private GameObject createBtn;
    void Start()
    {
        createBtn.SetActive(false);
        
    }

    public void CreateBtn()
    {
        createBtn.SetActive(true);
        createBtnAnim.speed = 0.79f;
    }

    private void OnDestroy()
    {
        createBtnAnim.StopPlayback();
    }
}
