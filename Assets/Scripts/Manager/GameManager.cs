using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ObjectPool ObjectPool;
    public StageSO[] stageInfo;

    public int curGold;

    //스테이지별 stage정보를 가지는 stageInfo배열. 어떤몬스터가 나올지 정해둔다.
}
