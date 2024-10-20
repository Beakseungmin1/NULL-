using UnityEngine;

namespace Assets._1._Scripts.CoopScripts.Objects
{
    enum GENSTATE
    {
        WAIT,
        WORK
    }
    internal class Generator : MonoBehaviour
    {
        [SerializeField] float maxPosX = 0f;
        [SerializeField] float minPosX = 0f;
        [SerializeField] float height  = 0f;
        [SerializeField] float currentSpawnScale = 0f;
        [SerializeField] float currentSpeed = 0f;
        [SerializeField] float levelUpScale = 0f;
        [SerializeField] float upSpeedScale = 0f;
        [SerializeField] float upSpawnScale = 0f;
        [SerializeField] float calculateScale = 0f;
        //--------------------------------------
        private Rigidbody2D rg2d;
        public static Generator instance;
        public GameObject genPrefab;

        //=
        Vector2 currentDestPos = Vector2.zero;
        GENSTATE currentState = GENSTATE.WAIT;
        private float calculTimer = 0f;
        private float levelTimer = 0f;
        private float spawnTimer = 0f;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            rg2d = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
           switch(currentState)
            {
                case GENSTATE.WAIT:
                    rg2d.velocity = Vector2.zero;
                    break;
                case GENSTATE.WORK:
                    GenOrderWork();
                    break;
            }
        }

        private void GenOrderWork()
        {
            if ((currentDestPos - rg2d.position).magnitude < 0.1f)
            {
                rg2d.velocity = Vector2.zero;
                transform.position = currentDestPos;
            }

            calculTimer += Time.deltaTime;
            if (calculTimer > calculateScale)
            {
                currentDestPos = new Vector2(Random.Range(minPosX, maxPosX), height);
                Vector2 dir = (currentDestPos - rg2d.position).normalized;

                rg2d.velocity = dir * currentSpeed;
                calculTimer = 0;
            }

            levelTimer += Time.deltaTime;
            if (levelTimer > levelUpScale)
            {
                currentSpeed = Mathf.Min(11f ,currentSpeed + upSpeedScale);
                currentSpawnScale = Mathf.Max(0.3f, currentSpawnScale - upSpawnScale);
                calculateScale = Mathf.Max(3f, calculateScale - 0.5f);
                levelTimer = 0;
            }

            spawnTimer += Time.deltaTime;
            if (spawnTimer > currentSpawnScale)
            {
                Instantiate(genPrefab, rg2d.position, Quaternion.identity);
                spawnTimer = 0;
            }
        }
        public void ChangeState(GENSTATE state)
        {
            currentState = state;
        }

    }
}
