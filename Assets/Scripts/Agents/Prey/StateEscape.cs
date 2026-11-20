using UnityEngine;

public class StateEscape : State
{
    public StateEscape(Animal agent, SteeringBehaviour steering, PreyUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
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
        _UI.ChangeMode(PreyMode.Escape);
        _anim.SetTrigger("IsWalking");
    }

    public override void Update()
    {
        if (_agent.HunterTarget == null)
        {
            _stateMachine.ChangeState(PreyMode.Flock);
            return;
        } 

        Evade();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsWalking");
    }

    private void Evade()
    {
        Vector3 desiredVelocity = _steering.CalculatedDirection(_agent, _steering.CalculatedFuture(_agent, _agent.HunterTarget));

        _agent.SetCurrentVelocity(_agent.CurrentVelocity + _steering.CalculatedSteering(_agent, -desiredVelocity));
    }
}
