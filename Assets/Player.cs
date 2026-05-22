using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    [Header("MoveMent details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8f;
    private float xInput;
    private bool facingRight = true;
    private bool canMove = true;
    private bool canJump = false;


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
       anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelotity", rb.linearVelocity.y);
        anim.SetBool("isGrounded", isGrounded);
      
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryToAttack();

    }

    //攻击方法
    private void TryToAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("attack");
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }


    // 跳跃方法
    private void TryToJump()
    {
        if(isGrounded && canJump)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    //移动
    private void HandleMovement()
    {
        if(canMove)
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }
    //检测是否在地面
    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    //人物反转
    private void HandleFlip()
    {
        if (rb.linearVelocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.linearVelocity.x < 0 && facingRight == true)
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
