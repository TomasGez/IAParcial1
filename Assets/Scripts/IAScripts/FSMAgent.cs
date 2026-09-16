using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum policeModes {Idle, Patrol}

public class FSMAgent : Agent
{
    private FSM stateMachine;

    private void Awake()
    {
        stateMachine = new FSM();

        IdleState idleState = new IdleState(stateMachine);

        stateMachine.RegisterState(policeModes.Idle, idleState);

        stateMachine.ChangeState(policeModes.Idle);
    }

    private void Update()
    {
        stateMachine.Update();
        
        transform.position += GetCurrentVelocity() * Time.deltaTime;

         if(GetCurrentVelocity() != Vector3.zero)
        {
            transform.forward = GetCurrentVelocity();
        }
    }
}
