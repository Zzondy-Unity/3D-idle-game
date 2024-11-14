public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerAutoMoveState AutoMoveState { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public float WalkSpeedModifier { get; set; } = 1f;
    public float RunSpeedModifier { get; set; } = 1f;
    public float DetectTerm { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        AutoMoveState = new PlayerAutoMoveState(this);
        IdleState = new PlayerIdleState(this);
        AttackState = new PlayerAttackState(this);

        WalkSpeedModifier = player.PlayerData.GroundData.WalkSpeedModifier;
        RunSpeedModifier = player.PlayerData.GroundData.RunSpeedModifier;
        DetectTerm = player.PlayerData.GroundData.DetectTerm;
    }
}