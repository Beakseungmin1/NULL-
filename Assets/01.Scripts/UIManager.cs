using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject SoundPanel;
    public GameObject BackToTitlePanel;
    public GameObject CharCreatePanel;

    //public Slider bgmSlider;
    //public Slider sfxSlider;

    /*public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        Instance = this;

        DontDestroyOnLoad(this);
    }*/
    private CharacterClass playerType = CharacterClass.Frog;

    void Update()
    {

    }

    public void StroyModButtonTrigger()
    {
        CharCreatePanel.SetActive(!CharCreatePanel.activeSelf);
    }

    public void GoTitlePanelTrigger()
    {
        BackToTitlePanel.SetActive(!BackToTitlePanel.activeSelf);
    }

    public void MultiPlayButton()
    {
        SceneManager.LoadScene("CoopScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void GoTitleButtonAccept()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void SoundButton()
    {
        SoundPanel.SetActive(true);
    }

    public void SoundExitButton()
    {
        SoundPanel.SetActive(false);
    }

    public void FrogButton()
    {
        playerType = CharacterClass.Frog;
    }

    public void PinkButton()
    {
        playerType = CharacterClass.PinkMan;
    }

    public void BlueButton()
    {
        playerType = CharacterClass.Virtual;
    }

    public void MaskButton()
    {
        playerType = CharacterClass.MaskDude;
    }

    public void SelectChar()
    {
        SceneManager.LoadScene("StageScene1");
        //TODO GameManager;
        GameManager.Instance.SetSelectedCharacter(playerType);
    }

}
