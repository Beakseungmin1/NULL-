using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : MonoBehaviour
{

    //void Update()
    //{
    //    if (gameObject.transform.position.y < -7)
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Floor"))
        this.gameObject.SetActive(false);
    }
}
