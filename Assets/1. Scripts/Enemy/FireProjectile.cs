using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Enemy
{
    private float speed = 15f;

    void Start()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        transform.up = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Floor"))
            this.gameObject.SetActive(false);
    }

}
