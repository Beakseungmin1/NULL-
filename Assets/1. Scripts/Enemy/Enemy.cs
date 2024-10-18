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
    float DropDelayTime = 0.3f; // 1���� �����Ÿ��
    float ShotDelayTime = 1f; // 2���� �����Ÿ��
    float patternTime = 10f; // ���� �����ð�

    bool isFire;
    Vector2 middlepositon;

    EnemyEnum.Enemypattern enemypattern;// = EnemyEnum.Enemypattern.pattern3;

    float toTime;  // �̵��� �ð� ���� ����
    float tooTime;  // ȭ��߻� �ð� ���� ����
    float toooTime = 10f; // ���� �ð� ���� ����

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
            // ���� ��ȯ���� ��ġ�� �����ش�
            GameObject ball = ObjectPool._instance.SpawnFromPool("Ball");

            // ���� ��ġ ���� (���� ��ġ + i)
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
            // �÷��̾�� ���� ���� ���� ���� ���
            Vector2 direction = (Player.transform.position - Weapon.transform.position).normalized;

            // ������ ȸ�� ���� ���
            float weaponangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���� ȸ�� ����
            Weapon.transform.rotation = Quaternion.Euler(0, 0, weaponangle);

            float spreadAngle = 16f; // ���� ����
            int numberOfProjectiles = 3; // �߻��� ����ü ��

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                // �� ����ü�� ���� ���
                float angleOffset = (i - 1) * spreadAngle; // -10, 0, +10
                Vector2 projectileDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

                // ������Ʈ Ǯ���� ȭ�� ��������
                GameObject Arrow = ObjectPool._instance.SpawnFromPool("Arrow");

                // ȭ�� ��ġ�� ȸ�� ����
                Arrow.transform.position = Weapon.transform.position;
                float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
                Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


                // �߻� �� Ÿ�̸� �ʱ�ȭ
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
                Debug.Log("�ǹ���");
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
