﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthReg : MonoBehaviour
{
    [SerializeField]
    private Animator animatoCreate;
    void Start()
    {
        animatoCreate = gameObject.GetComponent<Animator>();
        animatoCreate.SetTrigger("Active");
    }
}
