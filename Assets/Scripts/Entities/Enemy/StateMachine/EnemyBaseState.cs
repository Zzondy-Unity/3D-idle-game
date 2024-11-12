using UnityEngine.InputSystem;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected readonly EnemySO EnemyData;
    protected LayerMask TargetLayerMask => stateMachine.Enemy.TargetLayerMask;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        EnemyData = stateMachine.Enemy.EnemyData;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
        float speed = stateMachine.Enemy.EnemyData.Speed;
        stateMachine.Enemy.Agent.speed = speed;
        stateMachine.Enemy.animator.SetFloat(stateMachine.Enemy.AnimationData.SpeedParameterHash, speed);
    }


    protected void SetAnimation(int animationHash, bool animState)
    {
        stateMachine.Enemy.animator.SetBool(animationHash, animState);
    }

    protected void SetAnimation(int animationHash)
    {
        stateMachine.Enemy.animator.SetTrigger(animationHash);
    }

    protected bool IsInAttackDistance()
    {
        RaycastHit[] hits = InDistance(stateMachine.Enemy.EnemyData.AttackDistance);
        return hits.Length > 0;
    }

    protected RaycastHit[] InDistance(float range)
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(stateMachine.Enemy.transform.position, range, Vector3.up, 0f, TargetLayerMask);

        return hits;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }
}