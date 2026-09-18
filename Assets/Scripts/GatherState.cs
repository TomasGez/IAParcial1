using UnityEngine;

public class GatherState : State
{
    public GatherState (NPCAgent agent, GatherData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _gatherData = data;
    }

    private NPCAgent _agent;
    private GatherData _gatherData;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        if(_agent.GetCurrentBoidTarget() != null)
        {
            _agent.Pursuit(_agent.GetCurrentBoidTarget(), _gatherData.corpseMinDistance, _gatherData.corpseSlowingDistance);
        }
        else
        {
            _stateMachine.ChangeState(hunterModes.Patrol);
        }
    }

    public override void Exit()
    {

    }
}

[System.Serializable]
public class GatherData
{
    public float corpseMinDistance;
    public float corpseSlowingDistance;
}
