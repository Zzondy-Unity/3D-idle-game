using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedDealing;

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

        alreadyAppliedDealing = false;
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
        float normalizedTime = GetNormalizedTime(stateMachine.Player.animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Player.PlayerData.AttackData.Dealing_Start_TransitionTime)
            {
                stateMachine.Player.Weapon.ToggleWeaponCollider(true);
                stateMachine.Player.Weapon.Attack();
                alreadyAppliedDealing = true;
            }
            if (alreadyAppliedDealing && normalizedTime >= stateMachine.Player.PlayerData.AttackData.Dealing_End_TransitionTime)
            {
                stateMachine.Player.Weapon.ToggleWeaponCollider(false);
            }
        }


    }
}