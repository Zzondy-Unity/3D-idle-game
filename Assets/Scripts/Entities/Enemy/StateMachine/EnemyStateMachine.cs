using Unity.IO.LowLevel.Unsafe;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }


    public float WalkSpeed { get; set; } = 1f;
    public float RunSpeed { get; set; } = 1f;

    public EnemyIdleState IdleSate { get; }
    public EnemyChasingState ChasingState { get; }
    public EnemyAttackState AttackState { get; }


    public EnemyStateMachine(Enemy Enemy)
    {
        this.Enemy = Enemy;

        IdleSate = new EnemyIdleState(this);
        ChasingState = new EnemyChasingState(this);
        AttackState = new EnemyAttackState(this);

        WalkSpeed = Enemy.EnemyData.WalkSpeed;
        RunSpeed = Enemy.EnemyData.RunSpeed;
    }
}