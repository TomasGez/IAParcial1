using UnityEngine;

public abstract class State
{
    protected FSM _stateMachine;
    protected State(FSM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Enter() {}

    public virtual void Update() {}

    public virtual void Exit() {}
}
