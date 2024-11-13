using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    float AttackDelay;
    float LastAttackTime;
    private bool alreadyAppliedDealing;
    private bool isAttackCompleted;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        this.AttackDelay = EquipManager.Instance.Weapon.WeaponData.AttackDelay;
        LastAttackTime = 0f;
        alreadyAppliedDealing = false;
        isAttackCompleted = false;
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        alreadyAppliedDealing = false;
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);
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
            if (isAttackCompleted)
            {
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
        }
    }
}