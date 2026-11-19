using UnityEngine;

public class StateDead : State
{
    public StateDead(Animal agent, PreyUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Animal _agent;
    private PreyUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Dead);
    }

    public override void Update()
    {

    }

    public override void Exit() { }
}
