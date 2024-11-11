using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{

    float AttackDelay;
    float LastAttackTime;

    //전략패턴 적용해서 여러무기를 다양하게 사용

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.WalkSpeedModifier = 0f;
        this.AttackDelay = stateMachine.Player.Weapon.WeaponData.AttackDelay;
        SetAnimation(stateMachine.Player.AnimationData.AttackParameterHash, true);
    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(stateMachine.Player.AnimationData.AttackParameterHash, false);
    }

    public override void Update()
    {
        base.Update();
        LastAttackTime += Time.deltaTime;
        if(LastAttackTime > AttackDelay)
        {
            LastAttackTime = 0f;
            stateMachine.Player.Weapon.Attack();
            SetAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
        }
        //주변에 적이 없을 시 다시 이동
        if (!IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }


}
