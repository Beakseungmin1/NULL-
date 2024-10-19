using System.Collections;
using Assets._1._Scripts.CoopScripts.Objects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MAPSTATE
{
    NONE,
    PLAYGAME,
    ENDGAME,
}


public class CoopGameManager : MonoBehaviour
{
    [SerializeField]private float mainTime = 0;
    private int spawnid = 0;
    private float loaclTimer = 0f;

    private Dictionary<int, CoopPlayer> currentPlayers = new Dictionary<int, CoopPlayer>();
    private MAPSTATE mapState = MAPSTATE.NONE;    
    public GameObject playerPrefab;
    //============================================================================
    //UI SECTION
    public Text     TimerText;
    public Text     redPlayerHealthText;
    public Text     bluePlayerHealthText;
    public Image    redPlayerImage;
    public Image    bluePlayerImage;

    //============================================================================

    void Start()
    {
        CreateChar(PLAYERTYPE.FROG, new Vector2(-5, -7));
        CreateChar(PLAYERTYPE.BLUE, new Vector2(5, -7));
        Generator.instance.ChangeState(GENSTATE.WORK);
        SetInterfaceImage();
        ChangeMapState(MAPSTATE.PLAYGAME);
    }

    void Update()
    {
        loaclTimer += Time.deltaTime;
        TimerText.text = loaclTimer.ToString();

        switch(mapState)
        {
            case MAPSTATE.PLAYGAME:
                UpdateInterfaceText();
                break;
            case MAPSTATE.ENDGAME:
                break;
        }
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
}

