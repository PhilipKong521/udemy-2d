using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    public Collider2D[] enemyColliders; //创建数组时，使用设置大小 //faster// 可以添加或移动项目
    //public List<Collider2D> exampleList;  //创建列表，可以增加或缩小列表大小 // slower //可以添加或移动项目
    [Header("Attack details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask whatIsEnemy;

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

    public void DamageEnemies()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);

        foreach (Collider2D enemy in enemyColliders)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.TakeDamage();

            string enemyName = enemyScript.GetEnemyName();
            Debug.Log("I damaged enemy : " + enemyName);
        }
    }

    public void EnableMovementAndJump(bool enable)
    {
        canMove = enable;
        canJump = enable;
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
            anim.SetTrigger("attack");
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
        if (canMove)
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
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
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }
}
