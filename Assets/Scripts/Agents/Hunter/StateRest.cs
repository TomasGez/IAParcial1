using UnityEngine;

public class StateRest : State
{
    public StateRest(Archer agent, HunterUI UI, Animator anim, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
    }

    private Archer _agent;
    private HunterUI _UI;
    private Animator _anim;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Rest);
        _anim.SetTrigger("IsIdle");

        _agent.SetCurrentVelocity(Vector3.zero);
    }

    public override void Update()
    {
        _agent.RecoverEnergy();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsIdle");
    }
}
