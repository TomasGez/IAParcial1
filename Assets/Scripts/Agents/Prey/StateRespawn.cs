using UnityEngine;

public class StateRespawn : State
{
    public StateRespawn(Animal agent, PreyUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Animal _agent;
    private PreyUI _UI;

    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit() { }
}
