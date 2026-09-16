using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public PatrolState(PatrolData data, FSM stateMachine) : base(stateMachine)
    {
        _patrolData = data;
    }

    private PatrolData _patrolData;
    private int _currentNode;

    public override void Enter()
    {

    }

    public override void Update()
    {
        PatrolLoop();
    }

    public override void Exit()
    {
        
    }

    private void PatrolLoop()
    {
        Transform currentPoint = _patrolData.patrolPoints[_currentNode];

        if (Vector3.Distance(currentPoint.position, _patrolData._agent.transform.position) <= _patrolData.pointCheckDistance)
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

        _patrolData._agent.Seek(currentPoint.position);
    }
}

[System.Serializable]
public class PatrolData
{
    [HideInInspector] public Agent _agent;
    public List<Transform> patrolPoints;
    public float pointCheckDistance;
}