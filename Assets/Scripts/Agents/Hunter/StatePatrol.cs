using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{
    public StatePatrol(Archer agent, HunterUI UI, Animator anim, PatrolData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Archer _agent;
    private HunterUI _UI;
    private Animator _anim;
    private PatrolData _data;

    private int _index;
    private float _timer = 0;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Patrol);
        _anim.SetTrigger("IsWalking");

        _agent.SetCurrentSpeed(_agent.MaxSpeed);
    }

    public override void Update()
    {
        if (_agent.PreyTarget != null)
        {
            _stateMachine.ChangeState(HunterMode.Hunt);
            return;
        }

        if (_timer < _data.baitCooldown)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _data.baitCooldown)
        {
            _timer = 0;
            _stateMachine.ChangeState(HunterMode.Bait);
            return;
        }
        
        if (_data.waypoints.Count > 0) PatrolLoop();
    }

    public override void Exit()
    {
        _anim.ResetTrigger("IsWalking");
    }

    private void PatrolLoop()
    {
        Transform currentWaypoint = _data.waypoints[_index];

        if(Vector3.Distance(currentWaypoint.position, _agent.transform.position) <= _data.waypointCheckDistance)
        {
            _index = _index + 1 < _data.waypoints.Count ? _index + 1 : 0;
        }

        Vector3 direction = currentWaypoint.position - _agent.transform.position;

        _agent.SetCurrentVelocity(direction.normalized * _agent.CurrentSpeed);
    }
}

[System.Serializable]
public class PatrolData
{
    public List<Transform> waypoints;
    public float waypointCheckDistance;
    public float baitCooldown;
}