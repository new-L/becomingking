using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //РЕФАКТОРИТЬ

    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 12f;

    private bool isGrounded = false;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }


    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    //Поля, хранящие номареа слоев(для прыжка снизу платформы)
    private int character, collide;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        character = LayerMask.NameToLayer("Character");
        collide = LayerMask.NameToLayer("Collide");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = CharState.Idle;
        if (Input.GetKey(KeyCode.LeftShift)) { speed = 5f; }
        else speed = 3f;
        if (Input.GetButton("Horizontal")) Move();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        CheckPlatform();
    }

    private void Move()
    {
        State = CharState.Walking;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        isGrounded = colliders.Length > 1;
    }
    private void CheckPlatform()
    {
        if (rigidbody.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(character, collide, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(character, collide, false);
        }
    }

}

public enum CharState
{
    Idle,
    Walking
}
