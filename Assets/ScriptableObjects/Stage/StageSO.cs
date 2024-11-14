using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "DefaultStageSO", menuName = "Stage")]
public class StageSO : ScriptableObject
{
    public int stageIndex;
    public EnemySO[] monsters;
    //몬스터 종류를 달리하거나
    //스테이지별 스펙을 달리하거나
}
