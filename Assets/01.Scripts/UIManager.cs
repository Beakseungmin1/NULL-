using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum SCENESTATE
{

    
}

public class UIManager : MonoBehaviour
{
    public GameObject SoundPanel;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        Instance = this;

        DontDestroyOnLoad(this);
    }

    void Update()
    {
    }

    public void StroyModButton()
    {
        SceneManager.LoadScene("StageScene1");
    }

    public void MultiPlayButton()
    {
        SceneManager.LoadScene("CoopScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SoundButton()
    {
        SoundPanel.SetActive(true);
    }

    public void SoundExitButton()
    {
        SoundPanel.SetActive(false);
    }

    public void SelectChar()
    {
        
    }

}
