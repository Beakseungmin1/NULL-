using UnityEngine;

public class WallMove : MonoBehaviour
{
    public float speed = 2f; 

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
