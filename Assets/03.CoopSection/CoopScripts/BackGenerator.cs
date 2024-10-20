using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGenerator : MonoBehaviour
{
    [Header("배경 생성기 세팅")]
    [SerializeField] float spawnScale       = 0f; //얼마의 주기로?
    [SerializeField] float spawnBeginPosX   = 0f; //어느 X위치에서 시작?
    [SerializeField] float spawnDestroyPosX = 0f; //어느 X위치에 도달하면 삭제?
    [SerializeField] float spawnPosMinY     = 0f; //최소 높이는?
    [SerializeField] float spawnPosMaxY     = 0f; //최고 높이는?
    [SerializeField] float spawnVelociyMin  = 0f; //최소 속도는?
    [SerializeField] float spawnVelociyMax  = 0f; //최고 속도는?

    float localTimer = 0f;
    public GameObject[] backgroundPrefabs;
    public List<GameObject> backgroundObjects;

    void Update()
    {
        localTimer += Time.deltaTime;
        if(localTimer > spawnScale)
        {
            float posX      = spawnBeginPosX;
            float posY      = Random.Range(spawnPosMinY, spawnPosMaxY);
            float velocity  = Random.Range(spawnVelociyMin, spawnVelociyMax);
            int signal      = Random.Range(0, backgroundPrefabs.Length);
            
            GameObject spawnObject = Instantiate(backgroundPrefabs[signal], new Vector2(posX, posY), Quaternion.identity);
            spawnObject.transform.parent = this.transform;
            spawnObject.GetComponent<BackGroundDestroySelf>().Init(spawnDestroyPosX);
            backgroundObjects.Add(spawnObject);
            
            Rigidbody2D rg2d = spawnObject.GetComponent<Rigidbody2D>();
            rg2d.velocity = Vector2.left * velocity;
            localTimer = 0;
        }
    }
}
