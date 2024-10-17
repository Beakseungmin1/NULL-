using UnityEngine;

enum Stages
{
    STAGE1 = 1,
    STAGE2,
    STAGE3,
    STAGE4,
    STAGE5,
    STAGE6,
    LASTSTAGE
}
public class StageManager : MonoBehaviour
{
    public int currentStage = (int)Stages.STAGE1;

    public void StartStage(int stageNum)
    {
        currentStage = stageNum;
        switch (currentStage)
        {
            case (int)Stages.STAGE1:
                Stage1();
                break;
            case (int)Stages.STAGE2:
                Stage2();
                break;
            case (int)Stages.STAGE3:
                Stage3();
                break;
            case (int)Stages.STAGE4:
                Stage4();
                break;
            case (int)Stages.STAGE5:
                Stage5();
                break;
            case (int)Stages.STAGE6:
                Stage6();
                break;
            case (int)Stages.LASTSTAGE:
                Stage1();
                break;

        }
    }
    public void Stage1()
    {
        //스테이지 난이도?
    }
    public void Stage2()
    {
        //스테이지 난이도?
    }
    public void Stage3()
    {
        //스테이지 난이도?
    }
    public void Stage4()
    {
        //스테이지 난이도?
    }
    public void Stage5()
    {
        //스테이지 난이도?
    }
    public void Stage6()
    {
        //스테이지 난이도?
    }
    public void LastStage()
    {
        //스테이지 난이도?
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