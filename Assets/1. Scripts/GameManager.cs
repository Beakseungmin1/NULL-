using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text timerText;
    public Text scoreText;
    public Text HPText;

    public float timeLimit = 5.0f;
    private float timer;
    public int score; 
    private bool gamePlayState;
    
    public int playerHP = 5;

    [Range(0f, 1f)]
    public float mainSound = 1.0f;

    private GameSceneManager gameSceneManager;
    public CharaterSelect charaterSelect;
    CharacterClass characterClass;

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
        charaterSelect = GetComponent<CharaterSelect>();
        InitializeGame();
    }

    // Update is called once per frame
    private void Update()
    {
        //////////////////////////////////////////////////////// test1
        if (Input.GetKeyDown(KeyCode.C)) score++;
        if (Input.GetKeyDown(KeyCode.X)) playerHP--;
        scoreText.text = score.ToString();
        HPText.text = playerHP.ToString();
        SoundManager.instance.SetVolume(mainSound);
        //////////////////////////////////////////////////////// test1
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
    }
    //////////////////////////////////////////////////////// test2
    public void Select1()
    {
        charaterSelect.SetSelectedCharacter(CharacterClass.PinkMan);        
        Debug.Log(charaterSelect.GetSelectedCharacter());
        characterClass = charaterSelect.GetSelectedCharacter();

    }
    public void Select2()
    {
        charaterSelect.SetSelectedCharacter(CharacterClass.MaskDude);
        Debug.Log(charaterSelect.GetSelectedCharacter());
        characterClass = charaterSelect.GetSelectedCharacter();
    }
    public void Select3()
    {

    }
    public void DrowPlayer()
    {
        // 기존 플레이어가 아직 존재하는 경우 새로운 캐릭터를 생성하지 않음
        if (GameObject.FindWithTag("Player") != null)
        {
            Debug.Log("플레이어 오브젝트가 이미 존재합니다. 새로운 캐릭터를 생성하지 않습니다.");
            //return;
        }

        GameObject playerPrefab;
        switch (characterClass)
        {
            case CharacterClass.PinkMan:
                playerPrefab = Resources.Load<GameObject>("2. Prefabs/PlayerCharater/pinkPlayer");
                if (playerPrefab != null)
                {
                    Instantiate(playerPrefab, new Vector3(-13.15f, -0.97f, 0), Quaternion.identity);
                }
                break;

            case CharacterClass.MaskDude:
                playerPrefab = Resources.Load<GameObject>("2. Prefabs/PlayerCharater/maskPlayer");
                if (playerPrefab != null)
                {
                    Instantiate(playerPrefab, new Vector3(-13.15f, -0.97f, 0), Quaternion.identity);
                }
                break;
        }
    }
    public void DeleteCharater()
    {
        if (GameObject.FindWithTag("Player") != null) Destroy(GameObject.FindWithTag("Player"));
    }
    //////////////////////////////////////////////////////// test2
    private void StartScene(int SceneNumber)
    {
        Debug.Log(charaterSelect.GetSelectedCharacter());
        gameSceneManager.StartScene(SceneNumber);
        ResetSceneTimer();
        DrowPlayer(); ///////////test
        Debug.Log(charaterSelect.GetSelectedCharacter());
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
        GameObject.Find("UIManager").gameObject.transform.Find("Canvas").gameObject.SetActive(false);
        gamePlayState = true;
        StartScene((int)Scenes.SCENE_1);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("TitleScene");
        StartScene((int)Scenes.SCENE_1);
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
        TimerText();
    }
    private void TimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void GameExit() // 게임 종료
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
