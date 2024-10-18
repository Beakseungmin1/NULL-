//using UnityEngine;

//public class CharaterSelect : MonoBehaviour
//{

//     ------ UI 버튼 ------
//     ------ 버튼 인덱스 값에 따라 selectedClass 값 할당
//    public void SelectCharacter(int characterIndex)
//    {
//        CharacterClass selectedClass;

//        switch (characterIndex)
//        {
//            case 0:
//                selectedClass = CharacterClass.PinkMan;
//                break;
//            case 1:
//                selectedClass = CharacterClass.MiskDude;
//                break;
//            case 3:
//                selectedClass = CharacterClass.Virtual;
//                break;
//            case 4:
//                selectedClass = CharacterClass.;
//                break;
//        }

//        //GameManager.instance.SetSelectedCharacter(selectedClass);
//    }

//     ------ 게임 매니저에서 characterClass 값 저장하기 ------

//    private CharacterClass selectedCharacterClass;
//    public void SetSelectedCharacter(CharacterClass characterClass)
//    {
//        selectedCharacterClass = characterClass;
//    }

//    public CharacterClass GetSelectedCharacter()
//    {
//        return selectedCharacterClass;
//    }

//     ----- 게임 매니저에서 selectedClass 받아오기



//    private CharacterClass selectedClass = CharacterClass.MaskDude;

//    private void Start()
//    {
//        {
//            //CharacterClass selectedClass = GameManager.instance.GetSelectedCharacter();

//            switch (selectedClass)
//            {
//                case CharacterClass.PinkMan:
//                    Instantiate(Resources.Load<GameObject>("2.Prefabs/PlayerCharacter/pinkPlayer"));
//                    break;
//                case CharacterClass.MaskDude:
//                    Instantiate(Resources.Load<GameObject>("2.Prefabs/PlayerCharacter/maskPlayer"));
//                    break;

//                    //case CharacterClass.Virtual:
//                    //    Instantiate(Virtual);
//                    //    break;
//                    //case CharacterClass.:
//                    //    Instantiate();
//                    //    break;
//            }
//        }
//    }
//}



