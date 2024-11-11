using UnityEngine;

public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

public abstract class StateMachine
{
    private IState currentState;

    public void ChangeState(IState state)
    {
        Debug.Log($"���� ���� : {currentState}");
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
        Debug.Log($"���� ���� : {state}");
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}