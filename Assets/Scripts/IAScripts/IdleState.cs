using UnityEngine;

public class IdleState : State
{
    public IdleState(FSM stateMachine) : base(stateMachine) { }

    private float exitIdleTime = 3f;
    private float timer = 0f;

    public override void Enter()
    {
        timer = 0;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if(timer >= exitIdleTime)
        {
            stateMachine.ChangeState(policeModes.Patrol);
        }
    }

    public override void Exit()
    {
        
    }
}
