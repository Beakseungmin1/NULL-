using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    Text TitleText;

    //BUTTON CALLBACK FUNC()
    public void ExitButtonCallback()
    {
        //GameManager.Instance.GameExit();
        Application.Quit();
        Debug.Log("EXIT GAME");
    }
    public void CoopSceneButtonCallback()
    {
        //TODO 씬이동?
        //TEMP CODE
        SceneManager.LoadScene("CoopScene");
    }
}
