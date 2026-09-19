using UnityEngine;

public class DeadState : State
{
    public DeadState(BoidAgent agent, PreyUI UI, DeadData data, FSM stateMachine) : base(stateMachine)
    {
        _UI = UI;
        _agent = agent;
        _deadData = data;
    }

    private BoidAgent _agent;
    private PreyUI _UI;
    private DeadData _deadData;

    public override void Enter()
    {
        _UI.ChangePreyUI(preyModes.Dead);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}

[System.Serializable]
public class DeadData
{
    [HideInInspector] public Agent _agent;
}
