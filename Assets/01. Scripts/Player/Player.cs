using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public partial class Player : TopDownController
{
    private Camera _camera;
    private int HP = 3;
    private int maxHP = 3;

    TopDownMovement topDownMovement;

    public int PlayerHP
    {
        get { return HP; }
    }
    public int PlayerMaxHP
    {
        get { return maxHP; }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
        topDownMovement = GetComponent<TopDownMovement>();
    }


    // ----- 체력 ----- 
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
    }

    // ----- 이동 ----- 
    public void OnMove(InputValue value) // 좌/우 
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnJump(InputValue value) // 점프 
    {
        CallJumpEvent();
    }

    // ----- 맵 오브젝트 상호작용 ------ 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            HP -= 1;
            Vector2 attackerPosition = collision.transform.position;
            topDownMovement.Damage(attackerPosition);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Damage")) // 트랩과 충돌
        {
            HP -= 1;
            Vector2 attackerPosition = collision.transform.position;
            topDownMovement.Damage(attackerPosition);
        }

        if (collision.gameObject.CompareTag("Item")) // 아이템과 충돌 
        {
            if (HP < maxHP)
            { HP += 1; }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Destroy")) // 부서지는 블록과 충돌
        {
            Destroy(collision.gameObject, 1f);
            topDownMovement.BreakBlock();

        }
        if (collision.gameObject.CompareTag("Portal")) // 포탈과 충돌 
        {
            if (SceneManager.GetActiveScene().name == "StageScene1")
            {
                SceneManager.LoadScene("StageScene2");
            }
            else if (SceneManager.GetActiveScene().name == "StageScene2")
            {
                SceneManager.LoadScene("EndCutScene");
            }
        }
    }
}



