using System;
using UnityEngine;

public class PlayerChasingState : PlayerGroundState
{
    private bool isChasing = false;

    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        isChasing = false;
        stateMachine.RunSpeedModifier = 2f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Exit()
    {
        stateMachine.RunSpeedModifier = 1f;
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        Chase();
    }

    //함수제작
    private void Chase()
    {
        if (IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        if (!IsInDetectDistance())
        {
            stateMachine.ChangeState(stateMachine.AutoMoveState);
            return;
        }

        if (isChasing) return;

        isChasing = true;
        Player player = stateMachine.Player;

        RaycastHit[] detecteds = InDistance(player.PlayerData.GroundData.DetectDistance);

        float minDistance = float.MaxValue;
        Vector3 target = Vector3.zero;

        for (int i = 0; i < detecteds.Length; i++)
        {
            if ((player.transform.position - detecteds[i].transform.position).sqrMagnitude < minDistance)
            {
                minDistance = player.transform.position.sqrMagnitude;
                target = detecteds[i].transform.position;
            }
        }
        player.Agent.SetDestination(target);
    }
}