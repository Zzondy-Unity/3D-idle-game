using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "DefaultStageSO", menuName = "Stage")]
public class StageSO : ScriptableObject
{
    public int stageIndex;
    public EnemySO[] monsters;

}
