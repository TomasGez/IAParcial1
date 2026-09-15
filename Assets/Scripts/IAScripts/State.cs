using UnityEngine;

public abstract class State
{
    protected FSM stateMachine;
    protected State(FSM fsm)
    {
        stateMachine = fsm;
    }

    public virtual void Enter() {}

    public virtual void Update() {}

    public virtual void Exit() {}
}
