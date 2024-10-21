using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Phase1Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeText;

    private GameObject Player;
    private GameObject PlayerCamera;

    private float TTime;
    private float PhaTime = 20f;
    private float CountTime = 0;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        PlayerCamera.SetActive(false);
    }

    void Update()
    {
        TTime += Time.deltaTime;

        CountTime = PhaTime - TTime;

        TimeText.text = CountTime.ToString("F0");

        if (CountTime < 0)
        {
            Player.SetActive(false);
            CutSceneManager._instance.LoadNextCutscene();
        }
    }




}
