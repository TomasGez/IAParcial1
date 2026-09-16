using System.Collections.Generic;
using UnityEngine;

public class FlockState : State
{
    public FlockState(Agent agent, FlockData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _flockData = data;
    }

    private Agent _agent;
    private FlockData _flockData;

    public override void Enter()
    {

    }

    public override void Update()
    {
        Flocking();
    }

    public override void Exit()
    {
        
    }

    private Vector3 CalculatedSeparation()
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(var item in Spawner.Instance.GetAllAgents())
        {
            if(item == _agent) continue;

            if(Vector3.Distance(item.transform.position, _agent.transform.position) <= _flockData.separationRadius)
            {
                desiredVector += (item.transform.position - _agent.transform.position);
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        return _agent.CalculatedSteering(-desiredVector.normalized * _agent.GetMaxSpeed());
    }

    private Vector3 CalculatedAlignment()
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(var item in Spawner.Instance.GetAllAgents())
        {
            if(item == _agent) continue;

            if(Vector3.Distance(item.transform.position, _agent.transform.position) <= _flockData.alignmentRadius)
            {
                desiredVector += item.GetCurrentVelocity();
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        return _agent.CalculatedSteering(desiredVector.normalized * _agent.GetMaxSpeed());
    }

    private Vector3 CalculatedCohesion()
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(var item in Spawner.Instance.GetAllAgents())
        {
            if(item == _agent) continue;

            if(Vector3.Distance(item.transform.position, _agent.transform.position) <= _flockData.cohesionRadius)
            {
                desiredVector += item.transform.position;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        Vector3 desiredVelocity = _agent.CalculatedDirection(desiredVector);
        return _agent.CalculatedSteering(desiredVelocity.normalized * _agent.GetMaxSpeed());
    }

    private void Flocking()
    {
        Vector3 calculatedFlocking =
        CalculatedSeparation() * _flockData.separationWeight + 
        CalculatedAlignment() * _flockData.alignmentWeight + 
        CalculatedCohesion() * _flockData.cohesionWeight;

        _agent.AddToCurrentVelocity(calculatedFlocking);
    }
}

[System.Serializable]
public class FlockData
{
    public float separationRadius;
    public float alignmentRadius;
    public float cohesionRadius;
    [Range(0f, 1f)] public float separationWeight;
    [Range(0f, 1f)] public float alignmentWeight;
    [Range(0f, 1f)] public float cohesionWeight;
}
