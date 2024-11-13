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
        //체크용
        //실제로는 AutoMove가 켜지면

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