using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float balllifetime = 9.9f;
    private float time;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > balllifetime)
        {
            gameObject.SetActive(false);
        }

    }

    public void Launch(float angle, float speed)
    {
        // ���� �Ʒ� -1, -1
        // ������ �Ʒ� 1, -1
        Vector2 Shotdirection = new Vector2(angle * speed, -1 * speed);
        rigidbody2D.velocity = Shotdirection;
    }
}
