using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData grounData;
    protected LayerMask TargetLayerMask => stateMachine.Player.TargetLayerMask;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        grounData = stateMachine.Player.PlayerData.GroundData;
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
        float speed = stateMachine.Player.PlayerData.GroundData.Speed;
        stateMachine.Player.Agent.speed = speed;
        stateMachine.Player.animator.SetFloat(stateMachine.Player.AnimationData.SpeedParameterHash, speed);
    }

    protected virtual void AddInputActionCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;

        input.playerActions.Interact.started += OnInteractStarted;
    }

    protected virtual void RemoveInputActionCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;

        input.playerActions.Interact.started -= OnInteractStarted;
    }

    protected virtual void OnInteractStarted(InputAction.CallbackContext context)
    {

    }

    protected void SetAnimation(int animationHash, bool animState)
    {
        stateMachine.Player.animator.SetBool(animationHash, animState);
    }

    protected void SetAnimation(int animationHash)
    {
        stateMachine.Player.animator.SetTrigger(animationHash);
    }

    protected bool IsInDetectDistance()
    {
        RaycastHit[] hits = InDistance(stateMachine.Player.PlayerData.GroundData.DetectDistance);
        return hits.Length > 0;
    }

    protected bool IsInAttackDistance()
    {
        RaycastHit[] hits = InDistance(stateMachine.Player.Weapon.GetWeaponRange());
        return hits.Length > 0;
    }

    protected RaycastHit[] InDistance(float range)
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(stateMachine.Player.transform.position, range, Vector3.up, 0f, TargetLayerMask);

        return hits;
    }
}
