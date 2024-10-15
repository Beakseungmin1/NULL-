using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public enum EnemyPhase
{
    Phase1,
    Phase2,
    Phase3
}


public class Enemy : MonoBehaviour
{
    float MoveSpeed = 0.01f;
    float ThinkTime = 1f;
    float shitdelayTime = 1f;
    EnemyPhase enemyPhase = EnemyPhase.Phase1;


    float toTime;  //시간 계산용 변수
    float totoTime;  // 시간 계산용 변수2
    int random;

    //[SerializeField] private GameObject shit;




    void Update()
    {
        totoTime += Time.deltaTime;
        if (enemyPhase == EnemyPhase.Phase1)
        {
            whereigo();
            CreateShit();
        }


    }

    void CreateShit()
    {
        if (totoTime > shitdelayTime)
        {
            GameObject shit = ObjectPool._instance.SpawnFromPool("Shit");
            shit.transform.position = transform.position;
            //Instantiate(shit, gameObject.transform.position, Quaternion.identity);
            totoTime = 0;
        }
    }

    void whereigo()
    {
        toTime += Time.deltaTime;
        if (toTime > ThinkTime)
        {
            random = Random.Range(1, 3);
            toTime = 0;
        }
        if (random == 1)
        {
            MoveLeft();
        }
        if (random == 2)
        {
            MoveRight();
        }
        if (gameObject.transform.position.x > 8)
        {
            random = 1;
        }
        if (gameObject.transform.position.x < -8)
        {
            random = 2;
        }
    }


    void MoveLeft()
    {
        gameObject.transform.position +=  Vector3.left * MoveSpeed;
    }
    void MoveRight()
    {
        gameObject.transform.position += Vector3.right * MoveSpeed;
    }
}
