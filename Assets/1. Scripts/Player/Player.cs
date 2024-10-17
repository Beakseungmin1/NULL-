using UnityEngine;
using UnityEngine.InputSystem;


public partial class Player : TopDownController
{
    private Camera _camera;
    private int HP = 3;

    // 인스턴스 (게임매니저 필요) 
    public int PlayerHP
    {
        get { return HP; }
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
}



