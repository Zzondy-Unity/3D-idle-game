using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerGroundState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    float AttackDelay;
    //float LastAttackTime;
    private bool alreadyAppliedDealing;
    private bool isAttackCompleted;

    public override void Enter()
    {
        base.Enter();
        this.AttackDelay = EquipManager.Instance.Weapon.WeaponData.AttackDelay;
        //LastAttackTime = 0f;
        alreadyAppliedDealing = false;
        isAttackCompleted = false;
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        alreadyAppliedDealing = false;
    }

    public override void Update()
    {
        base.Update();
        //LastAttackTime += Time.deltaTime;
        //if (LastAttackTime > AttackDelay)
        //{
        //    LastAttackTime = 0f;
        //    Attack();
        //}
        Attack();

        if (!IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.AutoMoveState);
        }
    }

    private void Attack()
    {
        float normalizeTime = GetNormalizedTime(stateMachine.Player.animator, "Attack");
        if (normalizeTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizeTime >= stateMachine.Player.PlayerData.AttackData.Dealing_Start_TransitionTime)
            {
                EquipManager.Instance.Weapon.ToggleWeaponCollider(true);
                EquipManager.Instance.Weapon.Attack();
                alreadyAppliedDealing = true;
            }

            else if (alreadyAppliedDealing && normalizeTime >= stateMachine.Player.PlayerData.AttackData.Dealing_End_TransitionTime)
            {
                if (isAttackCompleted) return;

                EquipManager.Instance.Weapon.ToggleWeaponCollider(false);
                isAttackCompleted = true;
            }
        }
        else
        {
            stateMachine.ChangeState(stateMachine.AutoMoveState);
            return;
        }
    }
}
