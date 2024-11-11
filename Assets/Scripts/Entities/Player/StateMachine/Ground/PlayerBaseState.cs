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
}
