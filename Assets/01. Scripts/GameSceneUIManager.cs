using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneUIManager : MonoBehaviour
{
    public GameObject[] Hearts;
    private float localtimer = 0f;

    public GameObject BackToTitlePanel;
    private GameObject GameOverPanel;
    void Start()
    {
        GameManager.Instance.GetCurrentHealth();
    }

    void Update()
    {
        localtimer += Time.deltaTime;
        if (localtimer > 0.1f)
        {
            UpdateHealthUI();
            localtimer = 0f;
        }
    }

    public void UpdateHealthUI()
    {
        int currentHealth = GameManager.Instance.GetCurrentHealth();
        foreach (GameObject obj in Hearts) obj.SetActive(false);
        for (int i = 0; i < currentHealth; i++)
        {
            Hearts[i].SetActive(true);
        }
    }

    public void TitlePanelOn()
    {
        BackToTitlePanel.SetActive(true);
    }

    public void TitlePanelOff()
    {
        BackToTitlePanel.SetActive(false);
    }

    public void GoTitleButtonAccept()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void TestStage2Btn()
    {
        SceneManager.LoadScene("StageScene2");
    }
}



