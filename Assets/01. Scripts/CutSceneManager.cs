using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager _instance;

    public string[] cutsceneScenes = new string[] { "StartCutScene", "Phase1EndCutScene", "EndCutScene" }; // 컷신 씬 이름 배열
    private int currentCutsceneIndex = 0; // 현재 컷신 인덱스

    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void StartCutscene()
    {
        LoadCutscene(currentCutsceneIndex);
    }

    public void LoadNextCutscene()
    {
        currentCutsceneIndex++;
        if (currentCutsceneIndex < cutsceneScenes.Length)
        {
            LoadCutscene(currentCutsceneIndex);
        }
        else
        {
            // 모든 컷신이 끝났을 때의 처리 (예: 게임 시작)
            Debug.Log("모든 컷신이 끝났습니다.");
        }
    }

    private void LoadCutscene(int index)
    {
        SceneManager.LoadScene(cutsceneScenes[index]);
    }
}
