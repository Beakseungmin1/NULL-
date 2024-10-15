using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Text timerText;
    [SerializeField] int Score;
    float timer;
    float timeLimit = 10.0f;
    bool GamePlayState = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Score++;

        if (GamePlayState)
        {
            TimerText();
            if (timer > timeLimit || Score >= 10)
            {
                GameClear();
            }
            else
            {
                GameOver();
            }
        }
    }

    void ReStartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }

    void SceneChanger()
    {
        SceneManager.LoadScene("MainScene");
    }

    void GameClear()
    {
        Debug.Log("클리어");
        GamePlayState = false;
        GameClearUI();
    }
    void GameClearUI()
    {
        //gameObject.SetActive(true)
    }
    void GameOver()
    {
        Debug.Log("게임오버");
        GamePlayState = false;
        GameOverUI();
    }

    void GameOverUI()
    {
        //gameObject.SetActive(true)
    }

    void TimerText()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
