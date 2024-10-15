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
    //-------------------------------------------- //���� ���� ���
    private UIManager           instance;
    private SCENESTATE          sceneState; 
    private UIConnectHandler    connectHandler;

    [SerializeField] Text Score;
    //-------------------------------------------- //�� �������̽�
    [SerializeField] GameObject[] Interfaces;
    //-------------------------------------------- //����Ƽ �̺�Ʈ
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
    //-------------------------------------------- //�ܺο� �����Ǵ� �Լ�.
    /// <summary>
    /// [�̹μ�]UI���� �����Ǵ� Ư�� ��ư�� �������� ������ �Լ����� ����մϴ�.
    /// </summary>
    /// <returns></returns>
    public UIConnectHandler SetFunction() 
    {
        if (connectHandler == null) Debug.Log("UI�ڵ��� �������� ���߽��ϴ�.[�̹μ�]");
        return connectHandler; 
    }
    public UIManager GetUI() 
    {
        if (instance == null) Debug.Log("UIManager�� �ʱ�ȭ ���� ���߽��ϴ�.[�̹μ�]");
        return instance;
    }
    public void SetScoreText(int val)
    {
        Score.text = "SCORE:" + val.ToString();
    }
    //-------------------------------------------- //��ư�� ���εǴ� ���� �Լ� //���X
    /// <summary>
    /// ���X
    /// </summary>
    public void SoundButtonClicked()
    {
        connectHandler.SoundButtonInvoke();
    }
    /// <summary>
    /// ���X
    /// </summary>
    public void StartButtonCliecked()
    {
        connectHandler.StartButtonInvoke();
        sceneState = SCENESTATE.GAME;
    }
    /// <summary>
    /// ���X
    /// </summary>
    public void ExitButtonClicked()
    {
        connectHandler.ExitButtonInvoke();
    }
    //-------------------------------------------- //���� ������ ���Ǵ� �Լ�. // ����XXX
    void SetScene(SCENESTATE state)
    {
        GameObject temp = Interfaces[(int)state];
        if(temp.activeSelf == false) temp.SetActive(true);
    }
    //-------------------------------------------- //���� ������ ���Ǵ� �ھ� �Լ�. //����XXXX
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
