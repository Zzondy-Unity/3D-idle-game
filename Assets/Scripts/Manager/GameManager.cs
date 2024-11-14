using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool ObjectPool;
    public List<StageSO> stageInfo = new List<StageSO>();
    public StageSO curStage;

    public int curGold;
    public int playerLevel { get; private set; }

    public void LevelUp()
    {
        playerLevel++;
    }

    //StageManager?
    public void OnStageEnd()
    {
        curStage = null;
    }

    public void OnNextStage()
    {
        int nextStage = int.MaxValue;

        for(int i = 0; i < stageInfo.Count; i++)
        {
            nextStage = Mathf.Min(nextStage, stageInfo[i].stageIndex);
        }
        curStage = stageInfo[nextStage];
        stageInfo.RemoveAt(nextStage);
    }
}
