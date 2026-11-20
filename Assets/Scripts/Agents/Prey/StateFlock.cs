using UnityEngine;

public class StateFlock : State
{
    public StateFlock(Animal agent, SteeringBehaviour steering, PreyUI UI, Animator anim, FlockData data, FSM stateMachine) : base(stateMachine)
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
    private FlockData _data;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Flock);
        _anim.SetTrigger("IsWalking");

        _agent.SetCurrentSpeed(_agent.MaxSpeed);
        RandomDirection();
    }

    public override void Update()
    {
        if (_agent.HunterTarget != null)
        {
            _stateMachine.ChangeState(PreyMode.Escape);
            return;
        } 

        if (_agent.BaitTarget != null)
        {
            _stateMachine.ChangeState(PreyMode.Examine);
            return;
        } 

        if (_agent.NearbyPreys.Count > 0) Flocking();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsWalking");
    }

    private void RandomDirection()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f,Random.Range(-1f, 1f));
        _agent.SetCurrentVelocity(direction.normalized * _agent.MaxSpeed);
    }

    private void Flocking()
    {
        Vector3 calculated = _steering.CalculatedSeparation(_agent, _agent.NearbyPreys, _data.separationRadius) * _data.separationWeight +
                             _steering.CalculatedAlignment(_agent, _agent.NearbyPreys, _data.alignmentRadius) * _data.alignmentWeight +
                             _steering.CalculatedCohesion(_agent, _agent.NearbyPreys, _data.cohesionRadius) * _data.cohesionWeight;

        _agent.SetCurrentVelocity(_agent.CurrentVelocity + calculated);
    }
}

[System.Serializable]
public class FlockData
{
    public float separationRadius;
    public float alignmentRadius;
    public float cohesionRadius;
    [Range(0f, 1f)] public float separationWeight;
    [Range(0f, 1f)] public float alignmentWeight;
    [Range(0f, 1f)] public float cohesionWeight;
}