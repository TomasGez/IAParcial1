using UnityEngine;

public class AttackState : State
{
    public AttackState (NPCAgent agent, AttackData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _attackData = data;
    }

    private NPCAgent _agent;
    private AttackData _attackData;
    private float _attackTimer;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        if(_agent.GetCurrentBoidTarget() != null)
        {
            _agent.Pursuit(_agent.GetCurrentBoidTarget(), _attackData.preyMinDistance, _attackData.preySlowingDistance);

            if(Vector3.Distance(_agent.GetCurrentBoidTarget().transform.position, _agent.transform.position) > _attackData.losePreyDistance)
            {
                _agent.RemoveCurrentBoidTarget();
            }
        }
        else
        {
            _stateMachine.ChangeState(hunterModes.Patrol);
        }
    }

    public override void Exit()
    {
        _agent.SetIsHunting(false);
        _agent.ClearTargetQueue();
    }
}

[System.Serializable]
public class AttackData
{
    public float preyMinDistance;
    public float preySlowingDistance;
    public float losePreyDistance;
    public float AttackCooldown;
    public float rangeDistance;
    public float meleeDistance;
}
