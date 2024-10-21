using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EXITButton : MonoBehaviour
{
    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
