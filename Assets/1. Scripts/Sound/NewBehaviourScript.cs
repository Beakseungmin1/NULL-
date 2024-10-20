using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // 테스트용
    public Button TestButton;
    public void TestButtonClick()
    {
        SceneManager.LoadScene("StageScene1"); // 씬 로드
        SoundManager.instance.PlayBGM(Bgm.Stage1Bgm);
    }
}
