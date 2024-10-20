using UnityEngine;

public class BackGroundDestroySelf : MonoBehaviour 
{

    [SerializeField]private float deadLinePosX;
    [SerializeField]private float deadLinePosY;
    private float localTimer= 0;
    private float localTimeScale = 1;
    
    public void Init(float x)
    {
        deadLinePosX = x;
    }

    public void Update()
    {
        localTimer += Time.deltaTime;
        if(localTimer > localTimeScale)
        {
            if (this.transform.position.x < deadLinePosX || this.transform.position.y < deadLinePosY)
                Destroy(this.gameObject);
        }
    }
} 