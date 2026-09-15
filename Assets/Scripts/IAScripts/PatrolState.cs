using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private PatrolData patrolData;

    private int currentNode;
    private int patrolDirection = 1;

    public PatrolState(PatrolData data, FSM stateMachine) : base(stateMachine)
    {
        patrolData = data;
    }

    public override void Enter()
    {
        Debug.Log("Entre Patrol");
    }

    public override void Update()
    {
        if(patrolData.isPingPong) PatrolPingPong();
        else PatrolLoop();
    }

    public override void Exit()
    {
        Debug.Log("Sali Patrol");
    }

    private void PatrolLoop()
    {
        Transform currentWaypoint = patrolData.waypoints[currentNode];

        if(Vector3.Distance(currentWaypoint.position, patrolData.transform.position) <= patrolData.waypointCheckDistance)
        {
            //Condicional if y else de una linea
            currentNode = currentNode + 1 < patrolData.waypoints.Count ? currentNode + 1 : 0;
        }

        Vector3 direction = currentWaypoint.position - patrolData.transform.position;

        patrolData.agent.currentVelocity = direction.normalized * patrolData.maxSpeed;
    }

    private void PatrolPingPong()
    {
        Transform currentWaypoint = patrolData.waypoints[currentNode];

        if(Vector3.Distance(currentWaypoint.position, patrolData.transform.position) <= patrolData.waypointCheckDistance)
        {
            currentNode += patrolDirection;

            if(currentNode >= patrolData.waypoints.Count)
            {
                currentNode = patrolData.waypoints.Count - 1;
                patrolDirection = -1;
            }
            else if(currentNode < 0)
            {
                currentNode = 1;
                patrolDirection = 1;
            }
        }

        Vector3 direction = currentWaypoint.position - patrolData.transform.position;

        patrolData.agent.currentVelocity = direction.normalized * patrolData.maxSpeed;
    }
}

[System.Serializable]
public class PatrolData
{
    [HideInInspector] public Agent agent;
    public Transform transform;
    public List<Transform> waypoints;
    public float waypointCheckDistance;
    public float maxSpeed;
    public bool isPingPong;
}
