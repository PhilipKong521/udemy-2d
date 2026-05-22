using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    [Header("MoveMent details")]
    [SerializeField] private float xInput;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
                             private bool facingRight = true;

    [Header("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
                             private bool isGrounded;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        HandleCollision();
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

    //移动
    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    // 跳跃方法
    private void Jump()
    {
        if(isGrounded)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    //检测是否在地面
    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    //人物反转
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

    //使用OnDrawGizmos延Y轴向下画一条线
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}
