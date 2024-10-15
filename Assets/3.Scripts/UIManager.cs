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
        UpdateScene();
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
    //-------------------------------------------- //버튼에 매핑되는 공개되는 함수 사용X
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
    //-------------------------------------------- //내부 적으로 사용되는 함수. // 접근XXX
    void SetScene(SCENESTATE state)
    {
        if (sceneState == state) return;

        GameObject temp = Interfaces[(int)state];
        bool signal = temp.activeSelf;
        temp.SetActive(!signal);
    }
    //-------------------------------------------- //내부 적으로 사용되는 코어 함수. //접근XXXX
    void UpdateScene()
    {
        switch (sceneState)
        {
            case SCENESTATE.TITLE:
                SetScene(SCENESTATE.TITLE);
                break;
            case SCENESTATE.GAME:
                SetScene(SCENESTATE.GAME);
                break;
            case SCENESTATE.END:
                SetScene(SCENESTATE.END);
                break;
        }

        for (int i = 0; i < (int)SCENESTATE.LASTINDEX; i++)
        {
            if (i == (int)sceneState) continue;
            Interfaces[i].SetActive(false);
        }
    }
}
