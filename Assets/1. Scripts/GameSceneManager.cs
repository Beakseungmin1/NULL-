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
            case (int)Scenes.LASTSCENE:
                LastScene();
                break;
        }
    }

    public void Scene1()
    {
        SceneManager.LoadScene("StageScene1");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("StageScene1");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("StageScene1");
    }
    public void Scene4()
    {
        SceneManager.LoadScene("StageScene1");
    }
    public void Scene5()
    {
        SceneManager.LoadScene("StageScene1");
    }
    public void Scene6()
    {
        SceneManager.LoadScene("StageScene1");

    }
    public void LastScene()
    {
        SceneManager.LoadScene("StageScene1");
    }
    public void CompleteScene()
    {
        
        currentScene++;
    }

    public void SceneLevel()
    {
        //스테이지 난이도?
    }
}