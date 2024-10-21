using UnityEngine;
using UnityEngine.SceneManagement;

enum Scenes
{
    SCENE_1 = 1,
    SCENE_2,
    SCENE_3,
    SCENE_4,
    SCENE_5,
    SCENE_6,
    SCENE_7,
    LASTSCENE
}
public class GameSceneManager : MonoBehaviour
{
    public int currentScene = (int)Scenes.SCENE_1;

    public void StartScene(int SceneNum)
    {
        currentScene = SceneNum;
        switch (currentScene)
        {
            case (int)Scenes.SCENE_1:
                Scene1();
                break;
            case (int)Scenes.SCENE_2:
                Scene2();
                break;
            case (int)Scenes.SCENE_3:
                Scene3();
                break;
            case (int)Scenes.SCENE_4:
                Scene4();
                break;
            case (int)Scenes.SCENE_5:
                Scene5();
                break;
            case (int)Scenes.SCENE_6:
                Scene6();
                break;
            case (int)Scenes.SCENE_7:
                Scene7();
                break;
            case (int)Scenes.LASTSCENE:
                LastScene();
                break;
        }
    }

    public void Scene1()
    {
        GameManager.Instance.timeLimit = 9f;
        SceneManager.LoadScene("StartCutScene");
    }
    public void Scene2()
    {
        GameManager.Instance.timeLimit = 60f;
        SceneManager.LoadScene("[sing]EnemyPhase1");
    }
    public void Scene3()
    {
        GameManager.Instance.timeLimit = 23f;
        SceneManager.LoadScene("Phase1EndCutScene");
    }
    public void Scene4()
    {
        GameManager.Instance.timeLimit = 180f;
        SceneManager.LoadScene("StageScene1");
    }
    public void Scene5()
    {
        GameManager.Instance.timeLimit = 60f;
        SceneManager.LoadScene("[sing]EnemyPhase2");
    }
    public void Scene6()
    {
        GameManager.Instance.timeLimit = 180f;
        SceneManager.LoadScene("StageScene2");
    }
    public void Scene7()
    {
        GameManager.Instance.timeLimit = 60f;
        SceneManager.LoadScene("[sing]EnemyPhase3");
    }
    public void LastScene()
    {
        SceneManager.LoadScene("EndCutScene");
    }
    public void CompleteScene()
    {        
        currentScene++;
    }
}