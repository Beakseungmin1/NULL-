using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    /*
    public Text timerText;
    public Text scoreText;
    public Text HPText;
    */
    public float timeLimit = 5.0f;
    private float timer;
    public int score; 
    private bool gamePlayState;
    
    public int playerHP = 5;

    [Range(0f, 1f)]
    public float mainSound = 1.0f;

    private GameSceneManager gameSceneManager;

    [SerializeField] public GameObject[] CharacterPrefab;
    private GameObject currentCharacterInstance;
    CharacterClass characterClass = CharacterClass.Frog;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("StageScene") || scene.name.StartsWith("[sing]Enemy"))
        {
            SpawnCharacter();
        }
    }

    private void SpawnCharacter()
    {
        if (currentCharacterInstance != null)
        {
            Destroy(currentCharacterInstance);
        }

        currentCharacterInstance = Instantiate(CharacterPrefab[(int)characterClass], Vector3.zero, Quaternion.identity);
    }
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
        gameSceneManager = new GameSceneManager();        
        InitializeGame();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gamePlayState)
        {
            UadateTimer();
            if (timer > timeLimit)
            {
                GameClear();
            }

            if (playerHP <= 0)
            {
                GameOver();
            }
        }

        if(currentCharacterInstance != null)
        {
            playerHP = currentCharacterInstance.GetComponent<Player>().PlayerHP;
        }

    }
    private void StartScene(int SceneNumber)
    {
        gameSceneManager.StartScene(SceneNumber);
        ResetSceneTimer();
    }

    private void ResetSceneTimer()
    {
        timer = 0.0f;
    }

    private void InitializeGame()
    {
        score = 0;
        timer = 0.0f;
        gamePlayState = false;
    }
    public void StartGame()
    {
        GameObject.Find("Canvas").gameObject.SetActive(false);
        gamePlayState = true;
        StartScene((int)Scenes.SCENE_1);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void GameClear()
    {
        Debug.Log($"{gameSceneManager.currentScene} 스테이지 클리어");
        
        gamePlayState = false;

        //다음 씬의 사운드

        if (gameSceneManager.currentScene == (int)Scenes.LASTSCENE)
        {
            CompleteGame();
        }
        else
        {
            gamePlayState = true;
            gameSceneManager.CompleteScene();
            StartScene(gameSceneManager.currentScene);
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

        //게임오버 사운드

        gamePlayState = false;
        GameOverUI();
    }

    private void GameOverUI()
    {
        //gameObject.SetActive(true)
    }
    private void UadateTimer()
    {
        timer += Time.deltaTime;
        //TimerText();
    }
    /*
    private void TimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    */
    public void GameExit() // 게임 종료
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void SetSelectedCharacter(CharacterClass selectedClass)
    {
        characterClass = selectedClass;
    }

    public int GetCurrentHealth() { return playerHP; }
}
