using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAutoMoveState : PlayerGroundState
{
    private Transform Target;
    private float DetectTerm = 0.1f;
    private float LastDetect;

    public PlayerAutoMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.WalkSpeedModifier = 1f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        LastDetect += Time.deltaTime;
        if(LastDetect > DetectTerm)
        {
            LastDetect = 0f;
            Move();
        }
    }

    private void Move()
    {
        //주변에 적이있거나, 적한테 맞으면 쫒아감
        //없으면 전진
        float detectRange = stateMachine.Player.PlayerData.GroundData.DetectDistance;
        Vector3 playerPos = stateMachine.Player.transform.position;
        //주변에 적이 있다면 ChasingState로 변경
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
                //수동이동으로 전환
            }
        }
    }
}
