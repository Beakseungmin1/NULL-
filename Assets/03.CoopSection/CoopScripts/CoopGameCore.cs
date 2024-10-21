using System.Collections;
using Assets._1._Scripts.CoopScripts.Objects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum MAPSTATE
{
    CREATE,
    PLAYGAME,
    ENDGAME,
}

public enum Press
{
    LEFT,
    RIGHT
}

public class CoopGameCore : MonoBehaviour
{
    [SerializeField]private float mainTime = 0;
    private int spawnid = 0;
    private float loaclTimer = 0f;
    private bool[] readyboolean = new bool[2];
    private int[] inputs = new int[2];
    private int maxInput = 0;

    [SerializeField]private List<GameObject> generators = new List<GameObject>();
    private Dictionary<int, CoopPlayer> currentPlayers = new Dictionary<int, CoopPlayer>();
    List<Sprite> currentCharList = new List<Sprite>();
    public static CoopGameCore instance;
    private MAPSTATE mapState = MAPSTATE.CREATE;    
    public GameObject playerPrefab;
    //============================================================================
    //UI SECTION
    //In game UI
    public Text     TimerText;
    public Text     redPlayerHealthText;
    public Text     bluePlayerHealthText;
    public Image    redPlayerImage;
    public Image    bluePlayerImage;
    
    //Create char UI
    public Image    redshowImageSelect;
    public Image    blueshowImageSelect;
    public Button   redReadyButton;
    public Button   blueReadyButton;

    //End game UI
    public Text winnerText;
    public Image winnerImage;

    //Panel
    public GameObject createPanel;
    public GameObject inGamePanel;
    public GameObject endPanel;
    //============================================================================

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        readyboolean[0] = false; readyboolean[1] = false;
        currentCharList = ResourceHandler.instance.GetSprites();
        redshowImageSelect.sprite = currentCharList[0];
        blueshowImageSelect.sprite= currentCharList[0];
        maxInput = currentCharList.Count;      
    }

    void Update()
    {
        switch(mapState)
        {
            case MAPSTATE.CREATE:
                UpdateCreateState();
                UpdateAllMemReadey();
                break;
            case MAPSTATE.PLAYGAME:
                if (currentPlayers.Count <= 1)
                {
                    ChangeMapState(MAPSTATE.ENDGAME);
                    break;
                }
                loaclTimer += Time.deltaTime;
                TimerText.text = loaclTimer.ToString("F2");
                UpdateInterfaceText();
                break;
            case MAPSTATE.ENDGAME:
                UpdateEndGameState();
                break;
        }
    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary>
    /// 맵의 현재 상황을 변화 시킵니다.
    /// </summary>
    /// <param name="state"></param>
    public void ChangeMapState(MAPSTATE state)
    {
        mapState = state;
    }
    /// <summary>
    /// 플레이어를 관리자료에서 제거 합니다.
    /// </summary>
    /// <param name="id"></param>
    public void PopPlayerInDIct(int id)
    {
        currentPlayers.Remove(id);
    }
    /// <summary>
    /// 플레이어의 타입과 위치를 아규먼츠로 하여, 캐릭터를 생성합니다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    private void CreateChar(PLAYERTYPE type, Vector2 pos)
    {
        GameObject playerObject = Instantiate(playerPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        CoopPlayer player = playerObject.GetComponent<CoopPlayer>();
        player.InitChar(++spawnid, type);
        currentPlayers.Add(spawnid, player);
    }

    private void SetInterfaceImage()
    {
        redPlayerImage.sprite = ResourceHandler.instance.GetSprites()[(int)currentPlayers[1].GetCharType()]; 
        bluePlayerImage.sprite = ResourceHandler.instance.GetSprites()[(int)currentPlayers[2].GetCharType()]; 
    }
    private void UpdateInterfaceText()
    {
        redPlayerHealthText.text = currentPlayers[1].GetPlayerHealth().ToString();
        bluePlayerHealthText.text = currentPlayers[2].GetPlayerHealth().ToString();

    }
    
    private void UpdateCreateState()
    {
        if (!readyboolean[0])
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                inputs[0] = Math.Max(0, inputs[0] - 1);
                redshowImageSelect.sprite = currentCharList[inputs[0]];
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                inputs[0] = Math.Min(currentCharList.Count - 1, inputs[0] + 1);
                redshowImageSelect.sprite = currentCharList[inputs[0]];
            }
            else if(Input.GetKeyDown(KeyCode.F))
            {
                readyboolean[0] = true;
                redReadyButton.Select();
            }
            
        }
        if (!readyboolean[1])
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                inputs[1] = Math.Max(0, inputs[1] - 1);
                blueshowImageSelect.sprite = currentCharList[inputs[1]];
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                inputs[1] = Math.Min(currentCharList.Count - 1, inputs[1] + 1);
                blueshowImageSelect.sprite = currentCharList[inputs[1]];
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                readyboolean[1] = true;
                blueReadyButton.Select();
            }

        }
    }
    private void UpdateAllMemReadey()
    {
        if (readyboolean[0] && readyboolean[1])
        {
            CreateChar((PLAYERTYPE)inputs[0], new Vector2(-5, -7));
            CreateChar((PLAYERTYPE)inputs[1], new Vector2(5, -7));
            SetInterfaceImage();
            foreach(GameObject gen in generators)
            {
                gen.GetComponent<Generator>().ChangeState(GENSTATE.WORK);
            }
            createPanel.SetActive(false);
            ChangeMapState(MAPSTATE.PLAYGAME);
        }//ALL READY
    }
    
    private void UpdateEndGameState()
    {
        inGamePanel.SetActive(false);
        endPanel.SetActive(true);
        foreach (GameObject gen in generators)
        {
            gen.GetComponent<Generator>().ChangeState(GENSTATE.WAIT);
        }
        foreach (var winner in currentPlayers)
        {
            winnerText.text = winner.Value.GetPlayerId().ToString() + "P";
            winnerImage.sprite = ResourceHandler.instance.GetSprites()[(int)winner.Value.GetCharType()];
        }
    }
   
}

