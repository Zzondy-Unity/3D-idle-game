public class PlayerChasingState : PlayerAutoMoveState
{
    public PlayerChasingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.RunSpeedModifier = 2f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    
}