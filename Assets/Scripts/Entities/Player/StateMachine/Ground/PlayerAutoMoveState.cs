using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAutoMoveState : PlayerGroundState
{
    private Transform Target;
    private float DetectTerm = 0.5f;

    public PlayerAutoMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.WalkSpeedModifier = 1f;
        base.Enter();
        SetAnimation(stateMachine.Player.AnimationData.MoveParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(stateMachine.Player.AnimationData.MoveParameterHash, false);
    }

    public override void Update()
    {
        base.Update();
        Move();
    }

    private void Move()
    {
        //�ֺ��� �����ְų�, ������ ������ �i�ư�
        //������ ����
        float detectRange = stateMachine.Player.PlayerData.GroundData.DetectDistance;
        Vector3 playerPos = stateMachine.Player.transform.position;

        //�ֺ��� ���� �ִٸ� ChasingState�� ����
        if (Physics.CheckSphere(playerPos, detectRange, TargetLayerMask))
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        else
        {
            NavMeshPath path = new NavMeshPath();
            NavMeshAgent agent = stateMachine.Player.Agent;
            Vector3 nextStage = stateMachine.Player.nextStagePoint.position;

            if (agent.CalculatePath(nextStage, path))
            {
                agent.SetDestination(nextStage);
            }
            else
            {
                //�����̵����� ��ȯ
            }
        }
    }
}
