using UnityEngine;

public class EscapeState : State
{
    public EscapeState(BoidAgent agent, PreyUI UI, EscapeData data, FSM stateMachine) : base(stateMachine)
    {
        _UI = UI;
        _agent = agent;
        _escapeData = data;
    }

    private BoidAgent _agent;
    private PreyUI _UI;
    private EscapeData _escapeData;

    public override void Enter()
    {
        _UI.ChangePreyUI(preyModes.Escape);
        _agent.SetCurrentSpeed(_agent.GetMaxSpeed());
    }

    public override void Update()
    {
        if(_agent.GetIsDead())
        {
            _stateMachine.ChangeState(preyModes.Dead);
        }

        if(_agent.GetNPCTarget() != null)
        {
            Evade(_agent.GetNPCTarget());

            if(Vector3.Distance(_agent.GetNPCTarget().transform.position, _agent.transform.position) > _escapeData.fleeHunterDistance)
            {
                _stateMachine.ChangeState(preyModes.Flock);
            }
        }
        else
        {
            _stateMachine.ChangeState(preyModes.Flock);
        }
    }

    public override void Exit()
    {
        _agent.SetIsEscaping(false);
        _agent.SetIsExamining(false);
    }

    public void Evade(Agent target)
    {
        Vector3 desiredVelocity = _agent.CalculatedDirection(_agent.CalculatedFuture(target));

        _agent.AddToCurrentVelocity(_agent.CalculatedSteering(-desiredVelocity));
    }
}

[System.Serializable]
public class EscapeData
{
    public float fleeHunterDistance;
}
