public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.WalkSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        //üũ��
        //�����δ� AutoMove�� ������

        if (IsInAttackDistance())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.AutoMoveState);
            return;
        }
    }
}