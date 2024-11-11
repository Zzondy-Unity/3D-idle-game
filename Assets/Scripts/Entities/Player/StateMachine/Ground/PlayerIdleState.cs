public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.WalkSpeedModifier = 0f;
        base.Enter();
        SetAnimation(stateMachine.Player.AnimationData.IdleParameterHash, true);

    }

    public override void Exit()
    {
        base.Exit();
        SetAnimation(stateMachine.Player.AnimationData.IdleParameterHash, false);
    }

    public override void Update()
    {
        base.Update();
        //üũ��
        //�����δ� AutoMove�� ������
        stateMachine.ChangeState(stateMachine.AutoMoveState);
        return;
    }
}