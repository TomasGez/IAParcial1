using UnityEngine;

public class GatherState : State
{
    public GatherState (GatherData data, FSM stateMachine) : base(stateMachine)
    {
        _gatherData = data;
    }

    private GatherData _gatherData;

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
public class GatherData
{
    [HideInInspector] public Agent _agent;
    
}
