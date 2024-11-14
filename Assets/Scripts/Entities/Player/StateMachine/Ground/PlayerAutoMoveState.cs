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
        if (LastDetect > DetectTerm)
        {
            LastDetect = 0f;
            Detect();
        }
    }

    private void Detect()
    {
        RaycastHit[] detecteds = InDistance(EquipManager.Instance.Weapon.GetWeaponRange());

        float minDistance = float.MaxValue;
        Vector3 target = Vector3.zero;

        if (detecteds.Length < 1)
        {
            target = stateMachine.Player.nextStagePoint.position;
        }
        else
        {
            for (int i = 0; i < detecteds.Length; i++)
            {
                if ((stateMachine.Player.transform.position - detecteds[i].transform.position).sqrMagnitude < minDistance)
                {
                    minDistance = stateMachine.Player.transform.position.sqrMagnitude;
                    target = detecteds[i].transform.position;
                }
            }
            stateMachine.Player.Agent.SetDestination(target);
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }
}
