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
        SetAnimation(stateMachine.Player.AnimationData.MoveParameterHash, true);
    }

    public override void Exit()
    {
        stateMachine.RunSpeedModifier = 1f;
        base.Exit();
        SetAnimation(stateMachine.Player.AnimationData.MoveParameterHash, false);
    }

    public override void Update()
    {
        base.Update();
        Chase();
    }

    //�Լ�����
    private void Chase()
    {
        if (IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }

        if (!IsInDetectDistance())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        if (isChasing) return;

        isChasing = true;
        Player player = stateMachine.Player;

        RaycastHit[] detecteds = InDistance(player.PlayerData.GroundData.DetectDistance);

        float minDistance = float.MaxValue;
        Vector3 target = Vector3.zero;

        //������ ���� ����� ���� Ÿ������ => ������Ʈ������ �ص� �ǳ�?
        for (int i = 0; i < detecteds.Length; i++)
        {
            if ((player.transform.position - detecteds[i].transform.position).sqrMagnitude < minDistance)
            {
                minDistance = player.transform.position.sqrMagnitude;
                target = detecteds[i].transform.position;
                Debug.Log($"detected target is {detecteds[i]}\nposition is {target}");
            }
        }
        player.Agent.SetDestination(target);
    }
}