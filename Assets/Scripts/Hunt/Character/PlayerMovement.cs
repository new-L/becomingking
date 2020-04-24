using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject portalIn, portalOut;
    [SerializeField] private PlayerStatement playerDyingCheck;
    //Детекд порталов
    public static bool isPortalIn, isPortalOut;
    //Movement
    [SerializeField] protected float moveSpeed;
    protected float horizontalMove = 0f;

    private bool jump = false;
    private bool crouch = false;

    public UnityEvent EnergyBarChange;
    private void Awake()
    {
        portalIn = GameObject.FindGameObjectWithTag("PortalIn");
        portalOut = GameObject.FindGameObjectWithTag("PortalOut");
        playerDyingCheck = GetComponent<PlayerStatement>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        moveSpeed = 25f;
    }
    private void Update()
    {
        if (!playerDyingCheck.isDying)
        {
            animator.SetInteger("State", 0);
            horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Horizontal")) Sprint();
            if (Input.GetKeyUp(KeyCode.LeftShift)) animator.speed = 1f;
            if (Input.GetButton("Horizontal")) animator.SetInteger("State", 1);
            if (Input.GetButtonDown("Jump")) jump = true;
            if (Input.GetButton("Crouch")) crouch = true;
            else if (Input.GetButtonUp("Crouch")) crouch = false;
            if (!controller.Grounded) animator.SetInteger("State", 2);

            if ((isPortalIn || isPortalOut) && Input.GetKeyDown(KeyCode.F)) ToPortal();
        }
        
    }
    private void FixedUpdate()
    {
        if (!playerDyingCheck.isDying)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        else
        {
            controller.Move(0, false, false);
        }
    }

    private void ToPortal()
    {
        if (isPortalIn) controller.Rigidbody2D.position = new Vector2(portalOut.transform.position.x, portalOut.transform.position.y);
        else if (isPortalOut) controller.Rigidbody2D.position = new Vector2(portalIn.transform.position.x, portalIn.transform.position.y);
    }

    private void Sprint()
    {
        if (playerDyingCheck.CurrentEnergy > 0)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * 1.2f;
            playerDyingCheck.CurrentEnergy -= .1f;
            EnergyBarChange.Invoke();
            animator.speed = 1.8f;
        }
    }



}
