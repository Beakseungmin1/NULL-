using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndManager : MonoBehaviour
{
    public void LoadCutscene()
    {
        SceneManager.LoadScene("[sing]EnemyPhase1");
    }
}
