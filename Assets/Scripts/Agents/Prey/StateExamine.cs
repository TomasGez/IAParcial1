using UnityEngine;

public class StateExamine : State
{
    public StateExamine(Animal agent, SteeringBehaviour steering, PreyUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _steering = steering;
        _UI = UI;
        _anim = anim;
    }

    private Animal _agent;
    private SteeringBehaviour _steering;
    private PreyUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Examine);
        _anim.SetTrigger("IsWalking");
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

        if (_agent.BaitTarget != null) Seek();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsWalking");
    }

    private void Seek()
    {
        Vector3 desiredVelocity = _steering.CalculatedDirection(_agent, _agent.BaitTarget.transform.position);

        _agent.SetCurrentVelocity(_agent.CurrentVelocity + _steering.CalculatedSteering(_agent, desiredVelocity));
    }
}
