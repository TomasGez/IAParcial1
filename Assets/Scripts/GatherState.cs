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
    private float _harvestTimer = 0;

    public override void Enter()
    {
        _harvestTimer = 0;
    }

    public override void Update()
    {
        if(_agent.GetCurrentBoidTarget() != null)
        {
            _agent.Pursuit(_agent.GetCurrentBoidTarget(), _gatherData.corpseSlowingDistance, _gatherData.corpseMinDistance);

            _harvestTimer += Time.deltaTime;
            
            if(_harvestTimer > _gatherData.harvestCooldown)
            {
                _agent.GetCurrentBoidTarget().TerminatePrey();
                _stateMachine.ChangeState(hunterModes.Patrol);
            }
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
    public float corpseSlowingDistance;
    public float corpseMinDistance;
    public float harvestCooldown;
}
