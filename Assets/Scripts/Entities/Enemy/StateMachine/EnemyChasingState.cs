using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("적 추적상태 돌입");
        stateMachine.RunSpeed = 2f;
        base.Enter();
        SetAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash, true);
        SetAnimation(stateMachine.Enemy.AnimationData.MoveParameterHash, true);
    }

    public override void Exit()
    {
        stateMachine.RunSpeed = 1f;
        base.Exit();
        SetAnimation(stateMachine.Enemy.AnimationData.GroundParameterHash, false);
        SetAnimation(stateMachine.Enemy.AnimationData.MoveParameterHash, false);
    }

    public override void Update()
    {
        base.Update();
        Chase();
    }

    private void Chase()
    {
        Vector3 targetPos = CharacterManager.Instance.Player.transform.position;
        stateMachine.Enemy.Agent.SetDestination(targetPos);

        if (IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }
}
