using UnityEngine;
using System;


public class TopDownMovement : MonoBehaviour
{
    private TopDownController movementController;
    private Rigidbody2D movementRigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;

    private int jumpCount;
    private bool isGround;
    private Vector2 movementDirection = Vector2.zero;

    // ------ 초기화 -------
    private void Awake()
    {
        movementController = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ------ movementController 구독 ------
    private void Start()
    {
        movementController.OnMoveEvent += Move;
        movementController.OnJumpEvent += Jump;
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    // ------ 좌우 이동 ------
    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        // 멈추면 idel 
        if (direction.x == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else 
        {
            if (isGround)
                animator.SetBool("isRunning", true);
        }

        if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽
        }

        direction = direction * 5;
        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }

    // ------ 점프 ------
    private void Jump() 
    {
        if (jumpCount < 2) 
        {
            movementRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;

            if (jumpCount == 1) // 기본 점프
            {
                SoundManager.instance.PlaySFX(Sfx.S_JumpSfx);
                isGround = false; 
                animator.SetBool("isJumping", true);
                animator.SetBool("isRunning", false);
            }

            else if (jumpCount == 2) // 더블 점프 
            {
                SoundManager.instance.PlaySFX(Sfx.S_DoubleJumpSfx);
                animator.SetBool("isJumping", false);
                animator.SetBool("isDouble", true);
                animator.SetBool("isRunning", false);
            }
        }
    }

    // 충돌 확인 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isDouble", false);
            isGround = true; // 바닥에 닿았음을 설정
            jumpCount = 0;
        }
    } 
}


