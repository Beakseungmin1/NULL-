using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{

    float MoveSpeed = 0.015f;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += MoveSpeed * Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += MoveSpeed * Vector3.right;
        }
    }
}
