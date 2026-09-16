using UnityEngine;

public class DeadState : State
{
    public DeadState(DeadData data, FSM stateMachine) : base(stateMachine)
    {
        _deadData = data;
    }

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
