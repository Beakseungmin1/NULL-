using UnityEngine;

public class UpDownMove : MonoBehaviour
{
    public int speed = 1;
    public int topLength = 2;
    public int bottomLength = 2;
    public bool turn = true;
    Vector3 startPos;
    public int direction = 0; // 0이면 아래, 1이면 위 먼저  
    private void Start()
    {
        startPos = transform.position;
        turn = direction == 1;
    }
    private void Update()
    {
        if (transform.position.y > startPos.y + topLength)
            turn = false;

        if (transform.position.y < startPos.y - bottomLength)
            turn = true;

        transform.position += MoveVelue(turn);
    }
    Vector3 MoveVelue(bool Turn)
    {
        Vector3 move = new Vector3(0, speed, 0) * Time.deltaTime;
        if (Turn)
        {
            return move;
        }
        else
        {
            return -move;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어가 플랫폼과 같이 움직이기 위해 충돌하면 부모-자식 관계 설정
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 플레이어가 플랫폼에서 벗어나면 부모 관계 해제
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}