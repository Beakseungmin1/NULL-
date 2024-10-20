using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3pa : MonoBehaviour
{
    private GameObject Player;
    private Camera mainCamera;
    [SerializeField] private GameObject Weapon;
    EnemyEnum.Enemypattern enemypattern;

    float ShotDelayTime = 3f; // 2���� �����Ÿ��
    float patternTime = 10f; // ���� �����ð�

    float toTime;  // �̵��� �ð� ���� ����
    float tooTime;  // ȭ��߻� �ð� ���� ����
    float toooTime = 10f; // ���� �ð� ���� ����

    int fireProjectiles = 0;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = Camera.main;
    }

    void Update()
    {
        toTime += Time.deltaTime;
        tooTime += Time.deltaTime;
        toooTime += Time.deltaTime;

        PositionAboveCamera();

        switch (enemypattern)
        {
            case EnemyEnum.Enemypattern.pattern1:
                ShotGun();
                break;

            case EnemyEnum.Enemypattern.pattern2:
                Weapon.SetActive(false);
                Pas2FireProjectile();
                break;

            default:
                return;

        }


        SwitchPattern();

    }

    void PositionAboveCamera()
    {
        Vector2 targetposition = new Vector2(mainCamera.transform.position.x + 2f, mainCamera.transform.position.y + 3f);
        transform.position = Vector2.Lerp(transform.position, targetposition, 0.1f);
    }

    void ShotGun()
    {
        float delaytime = 0.1f; //�Ѿ˻����� �߻簣��
        int numberOfProjectiles = 5; // �߻��� ����ü ��

        if (tooTime > ShotDelayTime)
        {
            Weapon.SetActive(true);

            // �÷��̾�� ���� ���� ���� ���� ���
            Vector2 direction = (Player.transform.position - Weapon.transform.position).normalized;

            // ������ ȸ�� ���� ���
            float weaponangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // ���� ȸ�� ����
            Weapon.transform.rotation = Quaternion.Euler(0, 0, weaponangle);


            if (toTime > delaytime)
            {
                // ������Ʈ Ǯ���� �Ѿ� ��������
                GameObject Bullet = ObjectPool._instance.SpawnFromPool("Bullet");

                // �Ѿ� ��ġ�� ȸ�� ����
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
        // �÷��̾�� ���� ���� ���� ���� ���
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        float spreadAngle = 16f; // ���� ����
        int numberOfProjectiles = 5; // �߻��� ����ü ��

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // �� ����ü�� ���� ���
            float angleOffset = (i - 1) * spreadAngle; // -10, 0, +10
            Vector2 projectileDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

            // ������Ʈ Ǯ���� ȭ�� ��������
            GameObject Arrow = ObjectPool._instance.SpawnFromPool("Arrow");

            // ȭ�� ��ġ�� ȸ�� ����
            Arrow.transform.position = transform.position;
            float angle = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
            Arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


            // �߻� �� Ÿ�̸� �ʱ�ȭ
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
