using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3pa : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject Weapon;
    EnemyEnum.Enemypattern enemypattern = EnemyEnum.Enemypattern.pattern1;

    float ShotDelayTime = 3f; // 2패턴 드랍쿨타임
    float patternTime = 10f; // 패턴 유지시간

    bool isFire;

    float toTime;  // 이동용 시간 계산용 변수
    float tooTime;  // 화살발사 시간 계산용 변수
    float toooTime = 10f; // 패턴 시간 계산용 변수

    int fireProjectiles = 0;

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
                Shotmissile();
                break;

            case EnemyEnum.Enemypattern.pattern2:
                //Weapon.SetActive(false);
               // Pas2FireProjectile();
                break;

            default:
                return;

        }


       // SwitchPattern();

    }

    void Shotmissile()
    {
        int missiles = 7;

        if (!isFire)
        {

            for (int i = 0; i < missiles; i++)
            {
                GameObject Missile = ObjectPool._instance.SpawnFromPool("Missile");

                // 왼쪽벽부터 미사일 생성
                Missile.transform.position = new Vector2(-8.6f + (i * 2), 5);
            }
            isFire = true;
        }
    }

    void Shotmissile2()
    {
        int missiles = 7;

        if (!isFire)
        {

            for (int i = 0; i < missiles; i++)
            {
                GameObject Missile = ObjectPool._instance.SpawnFromPool("Missile");

                // 왼쪽벽부터 미사일 생성
                Missile.transform.position = new Vector2(-8.6f + (i * 2), 5);
            }
            isFire = true;
        }
    }





    void ShotGun()
    {
        float delaytime = 0.1f; //총알사이의 발사간격
        int numberOfProjectiles = 5; // 발사할 투사체 수

        if (tooTime > ShotDelayTime)
        {
            Weapon.SetActive(true);

            // 플레이어와 무기 간의 방향 벡터 계산
            Vector2 direction = (Player.transform.position - Weapon.transform.position).normalized;

            // 무기의 회전 각도 계산
            float weaponangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 무기 회전 설정
            Weapon.transform.rotation = Quaternion.Euler(0, 0, weaponangle);


            if (toTime > delaytime)
            {
                // 오브젝트 풀에서 총알 가져오기
                GameObject Bullet = ObjectPool._instance.SpawnFromPool("Bullet");

                // 총알 위치와 회전 설정
                Bullet.transform.position = Weapon.transform.position;
                Bullet.transform.rotation = Quaternion.Euler(0, 0, weaponangle);

                toTime = 0;
                delaytime += delaytime;
                fireProjectiles++;
            }
            if (fireProjectiles >= numberOfProjectiles)
            {
                tooTime = 0;
                fireProjectiles = 0;
            }
        }
    }



void Pas2FireProjectile()
{
    if (tooTime > ShotDelayTime)
    {
        // 플레이어와 무기 간의 방향 벡터 계산
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        float spreadAngle = 16f; // 각도 간격
        int numberOfProjectiles = 5; // 발사할 투사체 수

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // 각 투사체의 방향 계산
            float angleOffset = (i - 1) * spreadAngle; // -10, 0, +10
            Vector2 projectileDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

            // 오브젝트 풀에서 화살 가져오기
            GameObject Arrow = ObjectPool._instance.SpawnFromPool("Arrow");

            // 화살 위치와 회전 설정
            Arrow.transform.position = transform.position;
            float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
            Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


            // 발사 후 타이머 초기화
            tooTime = 0;
        }
    }
}

    void SwitchPattern()
    {
        if (toooTime > patternTime)
        {
            int ran = Random.Range(0, 2);
            enemypattern = (EnemyEnum.Enemypattern)ran;
            toooTime = 0;
        }
    }



}
