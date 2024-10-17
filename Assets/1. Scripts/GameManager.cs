using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Text timerText;
    [SerializeField] private int score;
    
    private float timer;
    private float timeLimit = 5.0f;
    private bool gamePlayState = true;
    public int playerHP = 5;

    private StageManager stageManager;

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
    private void Start()
    {
        stageManager = new StageManager();        
        InitializeGame();
        StartStage((int)Stages.STAGE1);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) score++;
        if (Input.GetKeyDown(KeyCode.X)) playerHP--;

        if (gamePlayState)
        {
            TimerText();
            if (timer > timeLimit)
            {
                GameClear();
            }

            if (playerHP <= 0)
            {
                GameOver();
            }
        }        
    }
    private void StartStage(int stageNumber)
    {
        stageManager.StartStage(stageNumber);
        ResetStageTimer();
    }
    private void ResetStageTimer()
    {
        timer = 0.0f;
    }

    private void InitializeGame()
    {
        score = 0;
        timer = 0.0f;
        gamePlayState = true;
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void SceneChanger()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void GameClear()
    {
        Debug.Log($"{stageManager.currentStage} 스테이지 클리어");
        gamePlayState = false;
        if (stageManager.currentStage == (int)Stages.LASTSTAGE)
        {
            CompleteGame();
        }
        else
        {
            gamePlayState = true;            
            stageManager.CompleteStage();
            StartStage(stageManager.currentStage);
        }
    }

    private void CompleteGame()
    {
        Debug.Log("모든 스테이지 클리어");
        GameClearUI();
    }

    private void GameClearUI()
    {
        //gameObject.SetActive(true)
    }
    private void GameOver()
    {
        Debug.Log("게임오버");
        gamePlayState = false;
        GameOverUI();
    }

    private void GameOverUI()
    {
        //gameObject.SetActive(true)
    }

    private void TimerText()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
