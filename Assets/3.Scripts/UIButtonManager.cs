using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonManager : MonoBehaviour
{
    private CharacterClass selectedClass;

    public void OnButtonClick(int index)
    {
        // 클래스 변수 selectedClass에 값 할당
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
                Debug.Log("잘못된 값, 기본 캐릭터 선택됨.");
                selectedClass = CharacterClass.MaskDude; //일단 임의로 넣었습니다
                break;
        }

        // 선택된 캐릭터를 GameManager에 저장
        //GameManager.instance.SetSelectedCharacter(selectedClass);
    }

    public void OnStartGameButton()
    {
        if (selectedClass != null)
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