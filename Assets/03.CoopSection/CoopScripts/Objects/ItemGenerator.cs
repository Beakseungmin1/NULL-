using System.Collections.Generic;
using UnityEngine;

namespace Assets._1._Scripts.CoopScripts.Objects
{
    internal class ItemGenerator : MonoBehaviour
    {
        [SerializeField] float spawnTimeScale;
        float localTimer = 0f;
        [SerializeField] List<GameObject> spawnPosition;
        [SerializeField] List<GameObject> items; 

        private void Update()
        {
            localTimer += Time.deltaTime;
            if(localTimer > spawnTimeScale)
            {
                int index = Random.Range(0, spawnPosition.Count);
                int itemindex = Random.Range(0, items.Count);
                Vector3 pos = spawnPosition[index].transform.position;
                Instantiate(items[itemindex], pos, Quaternion.identity);
                SoundManager.instance.PlaySFX(Sfx.M_FruitSfx);
                localTimer = 0;
            }
        }

    }
}
