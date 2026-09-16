using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum policeModes {Idle, Patrol}

public class FSMAgent : Agent
{
    [Header("Stats")]
    [SerializeField] private PatrolData patrolData;

    private FSM stateMachine;

    private void Awake()
    {
        patrolData.agent = this;
        stateMachine = new FSM();

        IdleState idleState = new IdleState(stateMachine);
        PatrolStateOld patrolState = new PatrolStateOld(patrolData, stateMachine);

        stateMachine.RegisterState(policeModes.Idle, idleState);
        stateMachine.RegisterState(policeModes.Patrol, patrolState);

        stateMachine.ChangeState(policeModes.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
        
        transform.position += _currentVelocity * Time.deltaTime;

         if(_currentVelocity != Vector3.zero)
        {
            transform.forward = _currentVelocity;
        }
    }
}
