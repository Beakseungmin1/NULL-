using Assets._3.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

enum SCENESTATE
{
    TITLE,
    GAME,
    END,
    LASTINDEX,
}


public class UIManager : MonoBehaviour
{
    //-------------------------------------------- //내부 변수 목록
    private UIManager           instance;
    private SCENESTATE          sceneState; 
    private UIConnectHandler    connectHandler;

    [SerializeField] GameObject settingsPanel; //-----------------------------------지윤 추가
    [SerializeField] Text Score;
    [SerializeField] Text Time;                //-----------------------------------지윤 추가
    //-------------------------------------------- //씬 인터페이스
    [SerializeField] GameObject[] Interfaces;
    //-------------------------------------------- //유니티 이벤트
    private void Awake()
    {
        if (connectHandler == null) 
            connectHandler = new UIConnectHandler();

        if (instance == null)
            instance = new UIManager();

        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        //UpdateScene();
    }
    //-------------------------------------------- //외부에 공개되는 함수.
    /// <summary>
    /// [이민석]UI에서 관리되는 특정 버튼이 눌렸을때 동작할 함수들을 등록합니다.
    /// </summary>
    /// <returns></returns>
    public UIConnectHandler SetFunction() 
    {
        if (connectHandler == null) Debug.Log("UI핸들이 생성되지 못했습니다.[이민석]");
        return connectHandler; 
    }
    public UIManager GetUI() 
    {
        if (instance == null) Debug.Log("UIManager가 초기화 되지 못했습니다.[이민석]");
        return instance;
    }
    public void SetScoreText(int val)
    {
        Score.text = "SCORE:" + val.ToString();
    }
    //-------------------------------------------- //버튼에 매핑되는 공개 함수 //사용X
    /// <summary>
    /// 사용X
    /// </summary>
    public void SoundButtonClicked()
    {
        connectHandler.SoundButtonInvoke();
    }
    /// <summary>
    /// 사용X
    /// </summary>
    public void StartButtonCliecked()
    {
        connectHandler.StartButtonInvoke();
        sceneState = SCENESTATE.GAME;
    }
    /// <summary>
    /// 사용X
    /// </summary>
    public void ExitButtonClicked()
    {
        connectHandler.ExitButtonInvoke();
    }

    public void SettingsButtonClicked() //-----------------------------------지윤 추가
    {
        //GameManager.Instance.PauseGame(); //설정 버튼을 누르면, 게임이 pause되는 것(삭제가능)
        settingsPanel.SetActive(true);
    }
    //-------------------------------------------- //내부 적으로 사용되는 함수. // 접근XXX
    //    void SetScene(SCENESTATE state)
    //    {
    //        GameObject temp = Interfaces[(int)state];
    //        if(temp.activeSelf == false) temp.SetActive(true);
    //    }
    //    //-------------------------------------------- //내부 적으로 사용되는 코어 함수. //접근XXXX
    //    void UpdateScene()
    //    {
    //        switch (sceneState)
    //        {
    //            case SCENESTATE.TITLE:
    //                SetScene(SCENESTATE.TITLE);
    //                break;
    //            case SCENESTATE.GAME:
    //                SetScene(SCENESTATE.GAME);
    //                break;
    //            case SCENESTATE.END:
    //                SetScene(SCENESTATE.END);
    //                break;
    //        }

    //        for (int i = 0; i < (int)SCENESTATE.LASTINDEX; i++)
    //        {
    //            if (i == (int)sceneState) continue;
    //            Interfaces[i].SetActive(false);
    //        }
    //    }
}
