using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float xInput;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private bool facingRight = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimation();
        HandleFlip();

    }

    private void HandleAnimation()
    {
       bool  isMoving= rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
      
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == true)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == false)
            Flip();
    }
    private void Flip()
    {
        transform.Rotate (0, 180, 0);
        facingRight = !facingRight;
    }
}
