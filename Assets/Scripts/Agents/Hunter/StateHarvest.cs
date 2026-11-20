using UnityEngine;

public class StateHarvest : State
{
    public StateHarvest(Archer agent, HunterUI UI, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
    }

    private Archer _agent;
    private HunterUI _UI;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Harvest);
    }

    public override void Update()
    {

    }

    public override void Exit() { }
}
