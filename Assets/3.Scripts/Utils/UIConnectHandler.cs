using System;
namespace Assets._3.Scripts.Utils
{
    public class UIConnectHandler
    {
        /// <summary>
        /// 사운드 온오프 기능과 관련된 함수를 모두 등록해주세요.
        /// </summary>
        public event Action SoundCallback; 
        /// <summary>
        /// 메인 메뉴에서 플레이 버튼 클릭과 관련된 함수를 모두 등록해주세요.
        /// </summary>
        public event Action PlayButtonCallback;
        /// <summary>
        /// 종료시에 수행되는 모든 함수를 등록 해주세요.
        /// </summary>
        public event Action ExitButtonCallback;


        /// <summary>
        /// 사용X
        /// </summary>
        public void SoundButtonInvoke()
        {
            SoundCallback?.Invoke();
        }
        /// <summary>
        /// 사용X
        /// </summary>
        public void StartButtonInvoke()
        {
            PlayButtonCallback?.Invoke();
        }
        /// <summary>
        /// 사용X
        /// </summary>
        public void ExitButtonInvoke()
        {
            ExitButtonCallback?.Invoke();
        }
    }  
}
