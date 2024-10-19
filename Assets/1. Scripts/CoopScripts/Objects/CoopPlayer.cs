using System;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public enum PLAYERTYPE
{
    FROG,
    MASK,
    PINK,
    BLUE,
}
public enum DIRECTION
{
    LEFT,
    RIGHT
}
public class CoopPlayer : MonoBehaviour
{
    [SerializeField] private float jumpVal = 0;
    [SerializeField] private float speedVal = 0;
    [SerializeField] private float attackDelay = 0;
    [SerializeField] private int playerLife = 0;
    //------------------------------------------
    private int     playerId = 0;
    private bool    isJump = false;
    private bool    isJumpPressed = false;
    private bool    isAttackPressed = false;
    private bool    isHit = false;
    private float   InputAxis = 0;
    private float   localTimer = 0f;
    private float   HitTimer = 0f;
    //------------------------------------------
    private Animator animator;
    private Rigidbody2D rg2d;
    private SpriteRenderer spriteRenderer;
    //------------------------------------------
    private DIRECTION direction = DIRECTION.RIGHT;
    private PLAYERTYPE playerType;
    //------------------------------------------
    public Text playerTagText;
    public GameObject projectile;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()//INPUT
    {
        localTimer += Time.deltaTime;
        
        InputUpdate();
        AttackUpdate();
        HitCheckUpate();
    }

    private void FixedUpdate()//PHYSIC
    {
        MoveUpdate();
    }

    private void LateUpdate()//ANIM
    {
        AnimUpdate();
    }

    public PLAYERTYPE GetCharType() { return playerType; }
    public int GetPlayerHealth() { return playerLife; }
    /// <summary>
    /// 캐릭터를 초기화 합니다.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    public void InitChar(int id, PLAYERTYPE type)
    {
        playerId = id;
        playerType = type;
        animator.runtimeAnimatorController = ResourceHandler.instance.GetAnims()[(int)type];
        spriteRenderer.sprite = ResourceHandler.instance.GetSprites()[(int)type];
        playerTagText.text = $"[{id}P]";
        playerTagText.color = playerId == 1 ? Color.red : Color.blue;
    }
    /// <summary>
    /// 해당하는 아이디의 캐릭터에게 입력을 전달합니다. [1]
    /// </summary>
    private void InputUpdate()
    {
        InputAxis = Input.GetAxisRaw(playerId == 1 ? "P1Horizontal" : "P2Horizontal");
        if (((Input.GetKeyDown(KeyCode.W) && playerId == 1) || (Input.GetKeyDown(KeyCode.UpArrow) && playerId == 2))
            && !isJump) isJumpPressed = true;
        if (((Input.GetKeyDown(KeyCode.F) && playerId == 1) || (Input.GetKeyDown(KeyCode.L) && playerId == 2))
            ) isAttackPressed = true;
    }
    /// <summary>
    /// 입력을 바탕으로 캐릭터를 이동 시킵니다. [2]
    /// </summary>
    private void MoveUpdate()
    {      
        rg2d.velocity = new Vector2(InputAxis * speedVal, rg2d.velocity.y);
        if (!(InputAxis == 0))
        {
            direction = InputAxis < 0 ? DIRECTION.LEFT : DIRECTION.RIGHT;
        }
        
        if (!isJump && isJumpPressed)
        {
            Debug.Log("Jump");
            rg2d.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
            //isJump = true;
            isJumpPressed = false;
        }       
    }
    /// <summary>
    /// 공격을 정의합니다.
    /// </summary>
    private void AttackUpdate()
    {
        if(localTimer > attackDelay && isAttackPressed)
        {
            localTimer = 0;
            isAttackPressed = false;
            animator.SetTrigger("isAttack");
            SpawnProjectile(direction);
            //TODO
        }
        else isAttackPressed = false;
    }
    private void SpawnProjectile(DIRECTION dir)
    {
        PlayerProjectile obj = Instantiate(projectile, this.transform.position, Quaternion.identity).GetComponent<PlayerProjectile>();
        obj.Init(dir, playerId);
    }

    //피격 처리..
    private void HitCheckUpate()
    {
        if (isHit) HitTimer += Time.deltaTime;
        if(HitTimer > 0.5f)
        {
            HitTimer = 0;
            isHit = false;
        }
    }
    private void IsHit()
    {
        isHit = true;
        --playerLife;
        Debug.Log($"피격받음 현재 체력 : {playerId}P:{playerLife}");

        if(playerLife <= 0)
        {
            IsDead();
        }
    }
    //회복 처리..
    private void IsConsume()
    {
        playerLife++;
        Debug.Log($"체력회복 현재 체력 : {playerId}P:{playerLife}"); ;
    }
    //사망 판정..
    private void IsDead()
    {
        Destroy(gameObject);
        //TODO
    }

    /// <summary>
    /// 입력처리 이후 애니메이션을 업데이트 합니다. [3]
    /// </summary>
    private void AnimUpdate()
    {
        switch(direction)
        {
            case DIRECTION.LEFT:
                spriteRenderer.flipX = true;
                break;
            case DIRECTION.RIGHT:
                spriteRenderer.flipX = false;
                break;
        }//FlipSprite
        
        //MoveAnim
        animator.SetBool("isJump", isJump);
        if (Mathf.Abs(InputAxis) == 1) animator.SetBool("isMove", true);
        else animator.SetBool("isMove", false);

        //HitAnim
        if(isHit == true) animator.SetTrigger("isHit");

    }
    /// <summary>
    /// 충돌 기반으로 플레이어의 점프 상태를 수정합니다.[fix]
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            int tempId = collision.gameObject.GetComponent<PlayerProjectile>().GetID();
            if (tempId != playerId)
            {
                Destroy(collision.gameObject);
                IsHit();
            }
        }

        if (collision.CompareTag("ProjectileGen"))
        {   
            Destroy(collision.gameObject);
            IsHit();       
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isJump = false;
        }

        if (collision.collider.CompareTag("Consume"))
        {
            Destroy(collision.gameObject);
            IsConsume();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJump = true;
        }
    }
}
