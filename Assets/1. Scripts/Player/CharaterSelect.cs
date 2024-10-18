//using UnityEngine;

//public class CharaterSelect : MonoBehaviour
//{

//     ------ UI 버튼 ------
//     ------ 버튼 인덱스 값에 따라 selectedClass 값 할당
//    public void SelectCharacter(int characterIndex) // 버튼에 들어가는 함수 
//    {
//        CharacterClass selectedClass;

//        switch (characterIndex)
//        {
//            case 0: // 버튼 0번 
//                selectedClass = CharacterClass.PinkMan;
//                break;
//            case 1: 
//                selectedClass = CharacterClass.MaskDude;
//                break;
//            case 2:
//                selectedClass = CharacterClass.Virtual;
//                break;
//            case 3:
//                selectedClass = CharacterClass.;
//                break;
//        }

//        GameManager.instance.SetSelectedCharacter(selectedClass);
//    }

//    private CharacterClass selectedCharacterClass;
//    public void SetSelectedCharacter(CharacterClass characterClass)
//    {
//        selectedCharacterClass = characterClass;
//    }

//    public CharacterClass GetSelectedCharacter()
//    {
//        return selectedCharacterClass;
//    }

//    private void Start()
//    {
//        {
//            CharacterClass selectedClass = GameManager.instance.GetSelectedCharacter();

//            switch (selectedClass)
//            {
//                case CharacterClass.PinkMan:
//                    Instantiate(Resources.Load<GameObject>("2.Prefabs/PlayerCharacter/pinkPlayer")); 
//                    break;
//                case CharacterClass.MaskDude:
//                    Instantiate(Resources.Load<GameObject>("2.Prefabs/PlayerCharacter/maskPlayer"));
//                    break;
//            }
//        }
//    }
//}



