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
        PatrolState patrolState = new PatrolState(patrolData, stateMachine);

        stateMachine.RegisterState(policeModes.Idle, idleState);
        stateMachine.RegisterState(policeModes.Patrol, patrolState);

        stateMachine.ChangeState(policeModes.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
        
        transform.position += currentVelocity * Time.deltaTime;

         if(currentVelocity != Vector3.zero)
        {
            transform.forward = currentVelocity;
        }
    }
}
