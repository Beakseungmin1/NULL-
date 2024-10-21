using UnityEngine;
using UnityEngine.InputSystem;


public partial class Player : TopDownController
{
    private Camera _camera;
    [SerializeField] private int HP = 3;

    // 인스턴스 (게임매니저 필요) 
    public int PlayerHP
    {
        get { return HP; }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
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
        // 개임매니저와 연결
        //Destroy(this.gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage")) // 트랩과 충돌
        {
            HP -= 1;

        }

        if (collision.gameObject.CompareTag("Item")) // 아이템과 충돌 
        {
            HP += 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Destroy")) // 부서지는 블록과 충돌
        {
            Destroy(collision.gameObject, 1f);
        }
    }
}



