using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public PatrolState(NPCAgent agent, PatrolData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _patrolData = data;
    }

    private NPCAgent _agent;
    private PatrolData _patrolData;
    private int _currentNode;
    private float _baitTimer;

    public override void Enter()
    {
        _baitTimer = _patrolData.baitStateChangeTime;
    }

    public override void Update()
    {
        PatrolLoop();

        _baitTimer -= Time.deltaTime;

        if(_baitTimer <= 0)
        {
            _baitTimer = _patrolData.baitStateChangeTime;
            _stateMachine.ChangeState(hunterModes.Bait);
        }
    }

    public override void Exit()
    {
        
    }

    private void PatrolLoop()
    {
        Transform currentPoint = _patrolData.patrolPoints[_currentNode];

        if (Vector3.Distance(currentPoint.position, _agent.transform.position) <= _patrolData.pointCheckDistance)
        {
            if(_currentNode + 1 < _patrolData.patrolPoints.Count)
            {
                _currentNode += 1;
            }
            else
            {
                _currentNode = 0;
            }
        }

        _agent.Seek(currentPoint.position);
    }
}

[System.Serializable]
public class PatrolData
{
    public List<Transform> patrolPoints;
    public float pointCheckDistance;
    public float baitStateChangeTime;
}