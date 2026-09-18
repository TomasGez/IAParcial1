using UnityEngine;

public class ExamineState : State
{
    public ExamineState(BoidAgent agent, ExamineData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _examineData = data;
    }

    private BoidAgent _agent;
    private ExamineData _examineData;

    public override void Enter()
    {

    }

    public override void Update()
    {
        if(_agent.GetIsDead())
        {
            _stateMachine.ChangeState(preyModes.Dead);
        }
        else if(_agent.GetIsEscaping())
        {
            _stateMachine.ChangeState(preyModes.Escape);
        }

        if(_agent.GetCurrentItem() != null)
        {
            Arrive(_agent.GetCurrentItem());

            if(Vector3.Distance(_agent.GetCurrentItem().transform.position, _agent.transform.position) <= _examineData.baitMinDistance)
            {
                _agent.GetCurrentItem().Interacting();
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
        _agent.SetIsExamining(false);
    }

    private void Arrive(InterestItem target)
    {
        float distance = Vector3.Distance(target.transform.position, _agent.transform.position);

        if(distance <= _examineData.baitMinDistance)
        {
            _agent.SetCurrentVelocity(Vector3.zero);
            return;
        }

        float stoppingDistance = distance - _examineData.baitMinDistance;
        float targetSpeed = _agent.GetMaxSpeed() * (stoppingDistance / _examineData.baitSlowingDistance);
        _agent.SetCurrentSpeed(Mathf.Min(targetSpeed, _agent.GetMaxSpeed()));

        Vector3 desiredVelocity = _agent.CalculatedDirection(target.transform.position);

        _agent.AddToCurrentVelocity(_agent.CalculatedSteering(desiredVelocity));
    }
}

[System.Serializable]
public class ExamineData
{
    public float baitSlowingDistance;
    public float baitMinDistance;
}
