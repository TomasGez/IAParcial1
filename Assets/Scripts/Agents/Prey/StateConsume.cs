using UnityEngine;

public class StateConsume : State
{
    public StateConsume(Animal agent, PreyUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Animal _agent;
    private PreyUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Consume);
    }

    public override void Update()
    {

    }

    public override void Exit() { }
}
