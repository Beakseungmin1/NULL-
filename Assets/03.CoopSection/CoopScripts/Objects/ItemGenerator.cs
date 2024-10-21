using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Assets._1._Scripts.CoopScripts.Objects
{
    internal class ItemGenerator : MonoBehaviour
    {
        [SerializeField] float spawnTimeScale;
        float localTimer = 0f;
        GENSTATE state = GENSTATE.WAIT;
        [SerializeField] List<GameObject> spawnPosition;
        [SerializeField] List<GameObject> items; 

        public void ChangeState(GENSTATE st)
        {
            state = st;
        }

        private void Update()
        {
            switch (state)
            {
                case GENSTATE.WAIT:
                    //TODO
                    break;
                case GENSTATE.WORK:
                    localTimer += Time.deltaTime;
                    if (localTimer > spawnTimeScale)
                    {
                        int index = Random.Range(0, spawnPosition.Count);
                        int itemindex = Random.Range(0, items.Count);
                        Vector3 pos = spawnPosition[index].transform.position;
                        Instantiate(items[itemindex], pos, Quaternion.identity);
                        SoundManager.instance.PlaySFX(Sfx.M_FruitSfx);
                        localTimer = 0;
                    }
                    break;
            }
            
        }

    }
}
