using UnityEngine;

public class AttackState : State
{
    public AttackState (AttackData data, FSM stateMachine) : base(stateMachine)
    {
        _attackData = data;
    }

    private AttackData _attackData;

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
public class AttackData
{
    [HideInInspector] public Agent _agent;
    
}
