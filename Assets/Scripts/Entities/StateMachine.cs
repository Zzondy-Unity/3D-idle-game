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
        Debug.Log($"현재 상태 : {currentState}");
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
        Debug.Log($"다음 상태 : {state}");
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