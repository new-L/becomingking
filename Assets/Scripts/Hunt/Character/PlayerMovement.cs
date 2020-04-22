using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Unit
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Animator animator;

    private bool jump = false;
    private bool crouch = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        moveSpeed = 25f;
    }

    private void Update()
    {
        animator.SetInteger("State", 0);
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (Input.GetButton("Horizontal")) animator.SetInteger("State", 1);
        if (Input.GetButtonDown("Jump")) jump = true;
        if (Input.GetButtonDown("Crouch")) crouch = true;
        else if (Input.GetButtonUp("Crouch")) crouch = false;
        if (!controller.Grounded) animator.SetInteger("State", 2);
    }

    private void FixedUpdate()
    {
        //Движение персонажа
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
