using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        this.AttackDelay = stateMachine.Player.Weapon.WeaponData.AttackDelay;
        LastAttackTime = 0f;
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
        if (LastAttackTime > AttackDelay)
        {
            LastAttackTime = 0f;
            Attack();
        }
        //�ֺ��� ���� ���� �� �ٽ� �̵�
        if (!IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    private void Attack()
    {
        SetAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        stateMachine.Player.Weapon.Attack();
    }

}
