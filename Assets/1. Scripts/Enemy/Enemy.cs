using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private GameObject Player;
    [SerializeField] private GameObject Weapon;


    float MoveSpeed = 0.01f;
    float ThinkTime = 0.8f;
    float DropDelayTime = 0.3f;
    float ShotDelayTime = 1f;

    EnemyPhase enemyPhase;
    Enemypattern enemypattern = Enemypattern.pattern2;

    float toTime;  //�ð� ���� ����
    float totoTime;  // �ð� ���� ����2
    int random;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        toTime += Time.deltaTime;
        totoTime += Time.deltaTime;


        if (enemypattern == Enemypattern.pattern1)
        {
            whereigo();
            DropProjectile();
        }
        else if (enemypattern == Enemypattern.pattern2)
        {
            whereigo();
            FireProjectile();
        }
        else
        {
            StandMiddle();
        }

    }

    void FireCircle()
    {
        int ballspeed = 5;

        for (int i = -1; i < 2; i += 2)
        { 
            GameObject ball = ObjectPool._instance.SpawnFromPool("Ball");
            Vector2 ballposition = new Vector2(transform.position.x + i, transform.position.y);
            ball.transform.position = ballposition;
        }
    }


    void FireProjectile()
    {
        if (totoTime > ShotDelayTime)
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
                //Arrow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg);
                float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
                Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


                // �߻� �� Ÿ�̸� �ʱ�ȭ
                totoTime = 0;
            }
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
        gameObject.transform.position +=  Vector3.left * MoveSpeed;
    }
    void MoveRight()
    {
        gameObject.transform.position += Vector3.right * MoveSpeed;
    }
    void StandMiddle()
    {
        Vector2 targetPosition = new Vector2(0, 4.3f);
        transform.position = Vector2.Lerp(transform.position, targetPosition, MoveSpeed);
    }
}
