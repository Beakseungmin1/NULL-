using UnityEngine;
using UnityEngine.InputSystem;


public partial class Player : TopDownController
{
    private Camera _camera;
    private int HP = 3;

    private void Awake()
    {
        _camera = Camera.main;
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
        // 오브젝트 삭제 말고 다른 이벤트 o
        Destroy(this.gameObject);
    }

    // ----- 이동 ----- 
    public void OnMove(InputValue value) // 좌/우 
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value) // 
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }
    public void OnJump(InputValue value) // 점프 
    {
        CallJumpEvent();
    }

    // 더블 점프
    // 달리기 
    // 낙하 
}



