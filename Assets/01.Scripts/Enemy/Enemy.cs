using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject Weapon;

    float MoveSpeed = 0.01f;
    float ThinkTime = 0.8f;
    float DropDelayTime = 0.3f; // 1패턴 드랍쿨타임
    float ShotDelayTime = 1f; // 2패턴 드랍쿨타임
    float patternTime = 10f; // 패턴 유지시간

    bool isFire;
    Vector2 middlepositon;

    EnemyEnum.Enemypattern enemypattern;// = EnemyEnum.Enemypattern.pattern3;

    float toTime;  // 이동용 시간 계산용 변수
    float tooTime;  // 화살발사 시간 계산용 변수
    float toooTime = 10f; // 패턴 시간 계산용 변수

    int random;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        toTime += Time.deltaTime;
        tooTime += Time.deltaTime;
        toooTime += Time.deltaTime;

        switch (enemypattern)
        {
            case EnemyEnum.Enemypattern.pattern1:
                whereigo();
                DropProjectile();
                break;

            case EnemyEnum.Enemypattern.pattern2:
                whereigo();
                FireProjectile();
                break;

            case EnemyEnum.Enemypattern.pattern3:
                Pattern3();
                break;

            default:
                return;

        }

        SwitchPattern();
        if (toooTime == 0)
        {
            isFire = false;
        }

    }

    void ShotBall()
    {
        float ballspeed = 10f;

        for (int i = -1; i < 2; i += 2)
        {
            // 공을 소환한후 위치를 정해준다
            GameObject ball = ObjectPool._instance.SpawnFromPool("Ball");

            // 공의 위치 설정 (현재 위치 + i)
            Vector2 ballposition = new Vector2(transform.position.x + i, transform.position.y);
            ball.transform.position = ballposition;

            Ball ballScript = ball.GetComponent<Ball>();

            ballScript.Launch(i, ballspeed);
        }


    }

    void FireProjectile()
    {
        if (tooTime > ShotDelayTime)
        {
            // 플레이어와 무기 간의 방향 벡터 계산
            Vector2 direction = (Player.transform.position - Weapon.transform.position).normalized;

            // 무기의 회전 각도 계산
            float weaponangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 무기 회전 설정
            Weapon.transform.rotation = Quaternion.Euler(0, 0, weaponangle);

            float spreadAngle = 16f; // 각도 간격
            int numberOfProjectiles = 3; // 발사할 투사체 수

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                // 각 투사체의 방향 계산
                float angleOffset = (i - 1) * spreadAngle; // -10, 0, +10
                Vector2 projectileDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

                // 오브젝트 풀에서 화살 가져오기
                GameObject Arrow = ObjectPool._instance.SpawnFromPool("Arrow");

                // 화살 위치와 회전 설정
                Arrow.transform.position = Weapon.transform.position;
                float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
                Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


                // 발사 후 타이머 초기화
                tooTime = 0;
            }
        }
    }


    void DropProjectile()
    {
        if (tooTime > DropDelayTime)
        {
            GameObject shit = ObjectPool._instance.SpawnFromPool("Shit");
            shit.transform.position = transform.position;
            tooTime = 0;
        }
    }

    void Pattern3()
    {
        Debug.Log(isFire);
        StandMiddle();

        if (Vector2.Distance(transform.position, middlepositon) < 0.1f)
        {
            if (!isFire)
            {
                Debug.Log("또뭔데");
                ShotBall();
                isFire = true;
            }
        }

    }


    void SwitchPattern()
    {
        if (toooTime > patternTime)
        {
            int ran = Random.Range(0, 3);
            enemypattern = (EnemyEnum.Enemypattern)ran;
            toooTime = 0;
        }
    }

    void whereigo()
    {
        if (toTime > ThinkTime)
        {
            random = Random.Range(1, 3);
            toTime = 0;
        }
        if (random == 1)
        {
            MoveLeft();
        }
        else if (random == 2)
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
        gameObject.transform.position += Vector3.left * MoveSpeed;
    }
    void MoveRight()
    {
        gameObject.transform.position += Vector3.right * MoveSpeed;
    }
    void StandMiddle()
    {
        middlepositon = new Vector2(0, 4.3f);
        transform.position = Vector2.Lerp(transform.position, middlepositon, MoveSpeed);
    }
}
