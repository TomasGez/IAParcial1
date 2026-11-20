using UnityEngine;

public class Archer : AgentHunter
{
    private HunterUI _hunterUI;

    protected override void Awake()
    {
        base.Awake();

        _hunterUI = GetComponentInChildren<HunterUI>();

        StatePatrol _statePatrol = new StatePatrol(this, _hunterUI, _stateMachine);
        StateBait _stateBait = new StateBait(this, _hunterUI, _stateMachine);
        StateHunt _stateHunt = new StateHunt(this, _hunterUI, _stateMachine);
        StateHarvest _stateHarvest = new StateHarvest(this, _hunterUI, _stateMachine);
        StateRest _stateRest = new StateRest(this, _hunterUI, _stateMachine);

        _stateMachine.RegisterState(HunterMode.Patrol, _statePatrol);
        _stateMachine.RegisterState(HunterMode.Bait, _stateBait);
        _stateMachine.RegisterState(HunterMode.Hunt, _stateHunt);
        _stateMachine.RegisterState(HunterMode.Harvest, _stateHarvest);
        _stateMachine.RegisterState(HunterMode.Rest, _stateRest);

        _stateMachine.StartFirstState(HunterMode.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }
}
