using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject Player;
    private Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(0f, 0f, -10f);
        
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        this.transform.position = Player.transform.position + offset;
    }
}
