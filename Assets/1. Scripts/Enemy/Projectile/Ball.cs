using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float balllifetime = 10;
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
        // 왼쪽 아래 -1, -1
        // 오른쪽 아래 1, -1
        rigidbody2D.velocity = new Vector2 (angle * speed, -1 * speed);
    }
}
