using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float speed = 0;
    int spawnerID = -1;
    Rigidbody2D rg2d;

    private void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    public void Init(DIRECTION dir, int id)
    {
        spawnerID = id;
        switch(dir)
        {
            case DIRECTION.LEFT:
                rg2d.velocity = Vector2.left * speed;
                break;
            case DIRECTION.RIGHT:
                rg2d.velocity = Vector2.right * speed;
                break;
        }
    }

    public int GetID() { return spawnerID; }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > 25f)
            Destroy(this.gameObject);
    }

   
}
