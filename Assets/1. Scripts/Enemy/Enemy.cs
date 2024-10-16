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

public enum Enemypattern
{
    pattern1,
    pattern2,
    pattern3
}


public class Enemy : MonoBehaviour
{
    protected GameObject Player;

    float MoveSpeed = 0.01f;
    float ThinkTime = 1f;
    float DropDelayTime = 0.3f;
    float ShotDelayTime = 0.5f;

    EnemyPhase enemyPhase = EnemyPhase.Phase2;
    Enemypattern enemypattern;

    float toTime;  //시간 계산용 변수
    float totoTime;  // 시간 계산용 변수2
    int random;

    //[SerializeField] private GameObject shit;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        totoTime += Time.deltaTime;
        if (enemyPhase == EnemyPhase.Phase1)
        {
            whereigo();
            DropProjectile();
        }
        else if (enemyPhase == EnemyPhase.Phase2)
        {
            whereigo();
            FireProjectile();
        }




    }

    void FireProjectile()
    {
        if (totoTime > ShotDelayTime)
        {
            //Vector2 direction = (Player.transform.position - transform.position).normalized;

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            GameObject Arrow = ObjectPool._instance.SpawnFromPool("Arrow");
            Arrow.transform.position = transform.position;
            totoTime = 0;
        }
    }


    void DropProjectile()
    {
        if (totoTime > DropDelayTime)
        {
            GameObject shit = ObjectPool._instance.SpawnFromPool("Shit");
            shit.transform.position = transform.position;
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
        if (gameObject.transform.position.x > 4)
        {
            random = 1;
        }
        if (gameObject.transform.position.x < -4)
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
