using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool ObjectPool;
    public StageSO[] stageInfo;

    public int curGold;

    //���������� stage������ ������ stageInfo�迭. ����Ͱ� ������ ���صд�.
}
