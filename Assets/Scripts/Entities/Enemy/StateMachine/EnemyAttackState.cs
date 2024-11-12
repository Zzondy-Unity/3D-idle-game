using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float LastAttackTime;
    private float AttackDelay;

    private bool alreadyAppliedDealing;


    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        this.AttackDelay = stateMachine.Enemy.EnemyData.AttackDelay;

        SetAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash, true);

        alreadyAppliedDealing = false;

    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash, false);
    }

    public override void Update()
    {
        base.Update();

        if (!IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
        }

        LastAttackTime += Time.deltaTime;
        if( LastAttackTime > AttackDelay)
        {
            LastAttackTime = 0f;
            Attack();
        }
    }

    private void Attack()
    {
        SetAnimation(stateMachine.Enemy.AnimationData.ComboAttackParameterHash);
        float normalizedTime = GetNormalizedTime(stateMachine.Enemy.animator, "Attack");
        if(normalizedTime < 1f)
        {
            if (!alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.EnemyData.Dealing_Start_TransitionTime)
            {
                stateMachine.Enemy.Weapon.ToggleWeaponCollider(true);
                alreadyAppliedDealing = true;
            }
            if(alreadyAppliedDealing && normalizedTime >= stateMachine.Enemy.EnemyData.Dealing_End_TransitionTime)
            {
                stateMachine.Enemy.Weapon.ToggleWeaponCollider(false);
            }
        }
    }
}
