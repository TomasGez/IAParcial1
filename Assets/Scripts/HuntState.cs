using UnityEngine;

public class HuntState : State
{
    public HuntState (NPCAgent agent, HuntData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _huntData = data;
    }

    private NPCAgent _agent;
    private HuntData _huntData;

    public override void Enter()
    {
        _agent.hunterUI.ChangeHunterUI(hunterModes.Hunt);
        _agent.hunterUI.IsMeleeAttack(true);
    }

    public override void Update()
    {
        _agent.Reloading();

        if(_agent.GetCurrentBoidTarget() != null)
        {
            _agent.Pursuit(_agent.GetCurrentBoidTarget(), _huntData.preySlowingDistance, _huntData.preyMinDistance);

            if(Vector3.Distance(_agent.GetCurrentBoidTarget().transform.position, _agent.transform.position) > _huntData.losePreyDistance)
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
    }
}

[System.Serializable]
public class HuntData
{
    public float preySlowingDistance;
    public float preyMinDistance;
    public float losePreyDistance;
    public float rangeDistance;
    public float meleeDistance;
}
