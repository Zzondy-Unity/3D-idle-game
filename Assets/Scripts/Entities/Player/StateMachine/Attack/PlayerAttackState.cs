using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{

    float AttackDelay;
    float LastAttackTime;

    //�������� �����ؼ� �������⸦ �پ��ϰ� ���

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
        //�ֺ��� ���� ���� �� �ٽ� �̵�
        if (!IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }


}
