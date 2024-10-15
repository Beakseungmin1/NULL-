using UnityEngine;
using System;


public class TopDownMovement : MonoBehaviour
{
    private TopDownController movementController;
    private Rigidbody2D movementRigidbody;
    private Animator animator;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundObject;

    private bool isGrounded;
    private Vector2 movementDirection = Vector2.zero;

    private void Awake()
    {
        movementController = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        if (!animator.GetBool("isJumping"))
        {
            movementRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
    }

    private void CheckIfGrounded()
    {
        if (movementRigidbody.velocity.y < 0)
        {
            Debug.DrawRay(movementRigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(movementRigidbody.position, Vector3.down, 1, LayerMask.GetMask("Ground"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("isJumping", false);
                }
            }
        }
    }
}


