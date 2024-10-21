using UnityEngine;

public class SawMove : MonoBehaviour
{
    public int speed = 1;
    public int leftLength = 5;
    public int rightLength = 5;
    public bool turn = true;
    public int direction = 0;
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
        turn = direction == 0;
    }
    private void Update()
    {
        if (transform.position.x > startPos.x + leftLength)
            turn = false;

        if (transform.position.x < startPos.x - rightLength)
            turn = true;

        transform.position += MoveVelue(turn);
    }

    Vector3 MoveVelue(bool Turn)
    {
        Vector3 move = new Vector3(speed, 0, 0) * Time.deltaTime;
        if (Turn)
        {
            return move;
        }
        else
        {
            return -move;
        }
    }
}
