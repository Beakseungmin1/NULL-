using System.Collections;
using Assets._1._Scripts.CoopScripts.Objects;
using System.Collections.Generic;
using UnityEngine;

public enum MAPSTATE
{
    NONE,
    SELECTCHAR,
    PLAYGAME,
    WINGAME,
    DEFEATGAME,
}


public class CoopGameManager : MonoBehaviour
{
    [SerializeField]private float mainTime = 0;
    private int spawnid = 0;

    private Dictionary<int, CoopPlayer> currentPlayers = new Dictionary<int, CoopPlayer>();
    private MAPSTATE mapState = MAPSTATE.NONE;    
    public GameObject playerPrefab;
    
    void Start()
    {
        CreateChar(PLAYERTYPE.FROG, new Vector2(-5, -7));
        CreateChar(PLAYERTYPE.BLUE, new Vector2(5, -7));
        CreateChar(PLAYERTYPE.PINK, new Vector2(5, -7));
        Generator.instance.ChangeState(GENSTATE.WORK);
    }

    void Update()
    {
        switch(mapState)
        {
            case MAPSTATE.NONE:
                break;
            case MAPSTATE.SELECTCHAR:
                break;
            case MAPSTATE.PLAYGAME:
                break;
            case MAPSTATE.WINGAME:
                break;
            case MAPSTATE.DEFEATGAME:
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
        playerObject.GetComponent<CoopPlayer>().InitChar(++spawnid, type);
    }
}
