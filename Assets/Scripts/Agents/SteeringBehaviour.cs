using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour
{
    public Vector3 CalculatedDirection(Agent agent, Vector3 targetPos)
    {
        Vector3 direction = (targetPos - agent.transform.position).normalized;
        direction *= agent.CurrentSpeed;

        return direction;
    }

    public Vector3 CalculatedSteering(Agent agent, Vector3 desiredDirection)
    {
        Vector3 steering = desiredDirection - agent.CurrentVelocity;

        steering = Vector3.ClampMagnitude(steering, agent.MaxSteering * Time.deltaTime);

        return steering;
    }

    public Vector3 CalculatedFuture(Agent agent, Agent target)
    {
        Vector3 direction = target.transform.position - agent.transform.position;
        float distance = direction.magnitude;

        float prediction = distance / (agent.CurrentSpeed + target.CurrentVelocity.magnitude);
        Vector3 futurePosition = target.transform.position + target.CurrentVelocity * prediction;
        return futurePosition;
    }

    public float CalculatedArrive(Agent agent, Vector3 targetPos, float SlowingDis, float MinimumDis)
    {
        float distance = Vector3.Distance(targetPos, agent.transform.position);

        if(distance <= MinimumDis)
        {
            agent.SetCurrentVelocity(Vector3.zero);
            return agent.MaxSpeed;
        }

        float stoppingDistance = distance - MinimumDis;
        float targetSpeed = agent.MaxSpeed * (stoppingDistance / SlowingDis);
        return Mathf.Min(targetSpeed, agent.MaxSpeed);
    }

    public Vector3 CalculatedSeparation(Agent agent, IReadOnlyList<Agent> agents, float radius)
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(Agent item in agents)
        {
            if(Vector3.Distance(item.transform.position, agent.transform.position) <= radius)
            {
                desiredVector += item.transform.position - agent.transform.position;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        return CalculatedSteering(agent, -desiredVector.normalized * agent.MaxSpeed);
    }

    public Vector3 CalculatedAlignment(Agent agent, IReadOnlyList<Agent> agents, float radius)
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(Agent item in agents)
        {
            if(Vector3.Distance(item.transform.position, agent.transform.position) <= radius)
            {
                desiredVector += item.CurrentVelocity;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        return CalculatedSteering(agent, desiredVector.normalized * agent.MaxSpeed);
    }

    public Vector3 CalculatedCohesion(Agent agent, IReadOnlyList<Agent> agents, float radius)
    {
        Vector3 desiredVector = default;
        int count = 0;

        foreach(Agent item in agents)
        {
            if(Vector3.Distance(item.transform.position, agent.transform.position) <= radius)
            {
                desiredVector += item.transform.position;
                count++;
            }
        }

        if(count == 0) return Vector3.zero;

        desiredVector /= count;
        Vector3 desiredVelocity = CalculatedDirection(agent, desiredVector);
        return CalculatedSteering(agent, desiredVelocity.normalized * agent.MaxSpeed);
    }
}
