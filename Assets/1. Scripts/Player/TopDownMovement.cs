using UnityEngine;
using System;


public class TopDownMovement : MonoBehaviour
{
    private TopDownController movementController;
    private Rigidbody2D movementRigidbody;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        movementController = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movementController.OnMoveEvent += Move;
        movementController.OnJumpEvent += Jump;
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
        CheckIfGrounded();
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;
        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }

    private void Jump()
    {
        movementRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false; // 점프 후 바닥에 닿지 않은 상태로 설정
        Debug.Log("Jump executed");
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Ground")); 
    }
}


