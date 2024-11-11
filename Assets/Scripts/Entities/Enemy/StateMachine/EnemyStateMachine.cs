using Unity.IO.LowLevel.Unsafe;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }


    public float WalkSpeed { get; set; } = 1f;
    public float RunSpeed { get; set; } = 1f;
    public float DetectTerm { get; }
    public float DetectDistance { get; }


    public EnemyStateMachine(Enemy Enemy)
    {
        this.Enemy = Enemy;



        WalkSpeed = Enemy.EnemyData.WalkSpeed;
        RunSpeed = Enemy.EnemyData.RunSpeed;
        DetectTerm = Enemy.EnemyData.DetectTerm;
        DetectDistance = Enemy.EnemyData.DetectDistance;
    }

}