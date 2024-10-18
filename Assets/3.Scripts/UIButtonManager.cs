using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject CharCreatePanel;

    //-------설정창 버튼-------//
    public void SettingsButtonClicked()
    {
        //GameManager.Instance.PauseGame(); //설정 버튼을 누르면, 게임이 pause되는 것(삭제가능)
        settingsPanel.SetActive(true);
    }

    public void ResumeGameButtonClicked()
    {
        //GameManager.Instance.ResumeGame(); // 게임 시간 다시 흐르게 함
        settingsPanel.SetActive(false);
    }

    //-------타이틀 씬 버튼-------//
    public void StorymodeButton()
    {
        CharCreatePanel.SetActive(true);
    }

    private CharacterClass selectedClass = CharacterClass.PinkMan; // 기본 캐릭터 설정
    private bool isCharacterSelected = false;  // 캐릭터 선택 여부 확인

    //-------캐릭터선택창 버튼-------//
    public void ReturnButton()
    {
        CharCreatePanel.SetActive(false);
    }

    public void OnButtonClick(int index)
    {
        switch (index)
        {
            case 0:
                selectedClass = CharacterClass.PinkMan;
                break;
            case 1:
                selectedClass = CharacterClass.MaskDude;
                break;
            case 2:
                selectedClass = CharacterClass.Virtual;
                break;
            default:
                selectedClass = CharacterClass.MaskDude; //일단 임의로 넣었습니다
                break;
        }
        isCharacterSelected = true;
        // 선택된 캐릭터를 GameManager에 저장
        //GameManager.instance.SetSelectedCharacter(selectedClass);
    }

    //-------게임시작 버튼-------//
    public void OnStartGameButton()
    {
        if (isCharacterSelected)
        {
            // 선택된 캐릭터가 존재해야 씬 전환
            Debug.Log("게임 시작. 선택된 캐릭터: " + selectedClass.ToString());
            SceneManager.LoadScene("StageScene1");
        }
        else
        {
            Debug.Log("캐릭터를 먼저 선택하세요.");
        }
    }
}