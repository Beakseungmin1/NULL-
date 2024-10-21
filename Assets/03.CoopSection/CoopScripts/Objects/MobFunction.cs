using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobFunction : MonoBehaviour
{
    [SerializeField] float minX     = 0f;
    [SerializeField] float maxX     = 0f;
    [SerializeField] float speed    = 0f;
    [SerializeField] int damage     = 0;
    bool isHit = false;
    bool isGrond = false;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    Vector2 destDir = Vector2.zero;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        destDir = new Vector2(Random.Range(-1, 2), 0);
        destDir.x = destDir.x == 0 ? -1 : destDir.x;
        if(destDir.x == 1)
            spriteRenderer.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(transform.position.x < minX || transform.position.x > maxX)
        {
            Destroy(transform.gameObject);
        }

        if(isHit)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("MobHit"))
            {
                if (stateInfo.normalizedTime >= 1f)
                {

                    Destroy(this.gameObject);
                }
            }
            
        }

    }

    private void FixedUpdate()
    {
        if (isGrond)
        {
            rb.velocity = new Vector2(destDir.x * speed, rb.velocity.y);
            anim.SetBool("isMove", true);
        }
    }

    public int GetDamage() { return damage; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGrond = true;
        }

        if (collision.collider.CompareTag("Player"))
        {
            isHit = true;
            anim.SetTrigger("isHit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Projectile"))
        {
            isHit = true;
            anim.SetTrigger("isHit");
            Destroy(collision.gameObject);
            SoundManager.instance.PlaySFX(Sfx.HitSfx);
        }
    }
}
