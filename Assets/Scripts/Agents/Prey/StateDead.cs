using UnityEngine;

public class StateDead : State
{
    public StateDead(Animal agent, PreyUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
    }

    private Animal _agent;
    private PreyUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(PreyMode.Dead);
        _anim.SetTrigger("IsDead");

        _agent.SetCurrentVelocity(Vector3.zero);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsDead");
    }
}
