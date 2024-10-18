using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;

    public void SettingsButtonClicked()
    {
        //GameManager.Instance.PauseGame(); //설정 버튼을 누르면, 게임이 pause되는 것(삭제가능)
        settingsPanel.SetActive(true);
    }

    private CharacterClass selectedClass = CharacterClass.PinkMan; // 기본 캐릭터 설정
    private bool isCharacterSelected = false;  // 캐릭터 선택 여부 확인
    public void OnButtonClick(int index)
    {
        // 캐락터 선택버튼
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