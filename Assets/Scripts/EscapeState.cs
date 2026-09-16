using UnityEngine;

public class EscapeState : State
{
    public EscapeState(EscapeData data, FSM stateMachine) : base(stateMachine)
    {
        _escapeData = data;
    }

    private EscapeData _escapeData;

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
public class EscapeData
{
    [HideInInspector] public Agent _agent;
}
