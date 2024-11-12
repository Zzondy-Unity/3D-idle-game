using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash, true);
        SetAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash, false);
        SetAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash, false);
    }

    public override void Update()
    {
        base.Update();

        stateMachine.ChangeState(stateMachine.ChasingState);
    }

    //쫄병은 직진, 보스만 따로지정
}
