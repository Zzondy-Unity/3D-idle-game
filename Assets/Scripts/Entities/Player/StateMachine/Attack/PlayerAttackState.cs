using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedDealing;

    float AttackDelay;
    float LastAttackTime;

    //전략패턴 적용해서 여러무기를 다양하게 사용

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        this.AttackDelay = stateMachine.Player.Weapon.WeaponData.AttackDelay;
        LastAttackTime = 0f;
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
        //주변에 적이 없을 시 다시 이동
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
            Debug.Log("Collider On at time: " + normalizedTime);
            stateMachine.Player.Weapon.Attack();
            alreadyAppliedDealing = true;
        }
        else if (alreadyAppliedDealing && normalizedTime >= stateMachine.Player.PlayerData.AttackData.Dealing_End_TransitionTime)
        {
            stateMachine.Player.Weapon.ToggleWeaponCollider(false);
            Debug.Log("Collider Off at time: " + normalizedTime);
            alreadyAppliedDealing = false; // 초기화
        }
    }
    else if (normalizedTime >= 1f) // 공격 종료 시 초기화
    {
        if (stateMachine.Player.Weapon.ColliderEnalbed)
        {
            stateMachine.Player.Weapon.ToggleWeaponCollider(false);
            Debug.Log("Collider forced off at end of attack.");
        }
        alreadyAppliedDealing = false; // 초기화
    }
}

}
