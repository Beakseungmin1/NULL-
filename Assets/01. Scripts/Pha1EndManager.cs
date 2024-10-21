using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Pha1EndManager : MonoBehaviour
{
    public void LoadCutscene()
    {
        SceneManager.LoadScene("StageScene1");
    }
}
