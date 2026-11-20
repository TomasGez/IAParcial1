using UnityEngine;

public class StateHarvest : State
{
    public StateHarvest(Archer agent, SteeringBehaviour steering, HunterUI UI, Animator anim, HarvestData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _steering = steering;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Archer _agent;
    private SteeringBehaviour _steering;
    private HunterUI _UI;
    private Animator _anim;
    private HarvestData _data;

    private float _timer;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Harvest);
        _anim.SetTrigger("IsIdle");

        _timer = 0;
        _UI.UpdateBar(_timer, _data.harvestTime, HunterMode.Harvest);
    }

    public override void Update()
    {
        if (_agent.PreyTarget == null || !_agent.PreyTarget.IsDead)
        {
            _stateMachine.ChangeState(HunterMode.Patrol);
            return;
        }

        if (_timer < _data.harvestTime)
        {
            _timer += Time.deltaTime;
            _UI.UpdateBar(_timer, _data.harvestTime, HunterMode.Harvest);
        }

        if (_timer >= _data.harvestTime)
        {
            _stateMachine.ChangeState(HunterMode.Patrol);
            _agent.PreyTarget.TerminateAnimal();
            _agent.PreyExited(_agent.PreyTarget);
            return;
        }

        if (_agent.PreyTarget != null) SeekArrive();
    }

    public override void Exit()
    {
        _timer = 0;

        _anim.ResetTrigger("IsIdle");
    }

    private void SeekArrive()
    {
        Vector3 desiredVelocity = _steering.CalculatedDirection(_agent, _agent.PreyTarget.transform.position);

        _agent.SetCurrentVelocity(_agent.CurrentVelocity + _steering.CalculatedSteering(_agent, desiredVelocity));

        _agent.SetCurrentSpeed(_steering.CalculatedArrive(_agent, _agent.PreyTarget.transform.position, _data.preySlowingDistance, _data.preyMinDistance));
    }
}

[System.Serializable]
public class HarvestData
{
    public float preySlowingDistance;
    public float preyMinDistance;
    public float harvestTime;
}