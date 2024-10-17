using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStage = 0;

    public void StartStage(int stageNum)
    {
        currentStage = stageNum;
    }

    public void CompleteStage()
    {
        currentStage++;
    }

    public void StageLevel()
    {
        //스테이지 난이도?
    }
}