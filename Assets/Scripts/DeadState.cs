using UnityEngine;

public class DeadState : State
{
    public DeadState(BoidAgent agent, DeadData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _deadData = data;
    }

    private BoidAgent _agent;
    private DeadData _deadData;

    public override void Enter()
    {

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
