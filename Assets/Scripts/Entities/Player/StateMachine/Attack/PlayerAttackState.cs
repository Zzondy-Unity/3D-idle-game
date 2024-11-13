using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        this.AttackDelay = EquipManager.Instance.Weapon.WeaponData.AttackDelay;
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
        //주변에 적이 없을 시 다시 이동
        if (!IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.AutoMoveState);
        }
    }

    private void Attack()
    {
        float normalizeTime = GetNormalizedTime(stateMachine.Player.animator, "Attack");
        if(normalizeTime < 1f)
        {
            if(normalizeTime >= stateMachine.Player.PlayerData.AttackData.Dealing_End_TransitionTime)
            {
                EquipManager.Instance.Weapon.ToggleWeaponCollider(false);
            }
            if(normalizeTime >= stateMachine.Player.PlayerData.AttackData.Dealing_Start_TransitionTime)
            {
                EquipManager.Instance.Weapon.ToggleWeaponCollider(true);
                EquipManager.Instance.Weapon.Attack();
                SetAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
            }
        }
    }
}
