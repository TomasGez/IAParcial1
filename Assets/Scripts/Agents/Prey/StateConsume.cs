using UnityEngine;

public class StateConsume : State
{
    public StateConsume(Animal agent, SteeringBehaviour steering, PreyUI UI, Animator anim, ConsumeData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _steering = steering;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Animal _agent;
    private SteeringBehaviour _steering;
    private PreyUI _UI;
    private Animator _anim;
    private ConsumeData _data;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Consume);
        _anim.SetTrigger("IsIdle");
    }

    public override void Update()
    {
        if (_agent.HunterTarget != null)
        {
            _stateMachine.ChangeState(PreyMode.Escape);
            return;
        } 

        if (_agent.BaitTarget == null)
        {
            _stateMachine.ChangeState(PreyMode.Flock);
            return;
        }

        if (_agent.BaitTarget != null)
        {
            _agent.BaitTarget.ConsumeBait();
            SeekArrive();
        }
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsIdle");
    }

    private void SeekArrive()
    {
        Vector3 desiredVelocity = _steering.CalculatedDirection(_agent, _agent.BaitTarget.transform.position);

        _agent.SetCurrentVelocity(_agent.CurrentVelocity + _steering.CalculatedSteering(_agent, desiredVelocity));

        _agent.SetCurrentSpeed(_steering.CalculatedArrive(_agent, _agent.BaitTarget.transform.position, _data.baitSlowingDistance, _data.baitMinDistance));
    }
}

[System.Serializable]
public class ConsumeData
{
    public float baitSlowingDistance;
    public float baitMinDistance;
}