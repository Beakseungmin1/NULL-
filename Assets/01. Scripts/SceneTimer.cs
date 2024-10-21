using UnityEngine;
using UnityEngine.UI;

public class SceneTimer :MonoBehaviour
{
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
        GameManager.Instance.timerText = text;
    }
}