using UnityEngine;

public class Animal : AgentPrey
{
    private PreyUI _preyUI;

    protected override void Awake()
    {
        base.Awake();

        _preyUI = GetComponentInChildren<PreyUI>();

        StateFlock _stateFlock = new StateFlock(this, _preyUI, _stateMachine);
        StateExamine _stateExamine = new StateExamine(this, _preyUI, _stateMachine);
        StateConsume _stateConsume = new StateConsume(this, _preyUI, _stateMachine);
        StateEscape _stateEscape = new StateEscape(this, _preyUI, _stateMachine);
        StateDead _stateDead = new StateDead(this, _preyUI, _stateMachine);
        StateRespawn _stateRespawn = new StateRespawn(this, _preyUI, _stateMachine);


        _stateMachine.RegisterState(PreyMode.Flock, _stateFlock);
        _stateMachine.RegisterState(PreyMode.Examine, _stateExamine);
        _stateMachine.RegisterState(PreyMode.Consume, _stateConsume);
        _stateMachine.RegisterState(PreyMode.Escape, _stateEscape);
        _stateMachine.RegisterState(PreyMode.Dead, _stateDead);
        _stateMachine.RegisterState(PreyMode.Respawn, _stateRespawn);

        _allPreys.Add(this);

        _stateMachine.StartFirstState(PreyMode.Flock);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }
}
