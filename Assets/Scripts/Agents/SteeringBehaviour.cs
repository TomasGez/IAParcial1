using UnityEngine;

public class SteeringBehaviour
{
    protected Vector3 CalculatedDirection(Agent agent, Vector3 targetPos)
    {
        Vector3 direction = (targetPos - agent.transform.position).normalized;
        direction *= agent.CurrentSpeed;

        return direction;
    }

    protected Vector3 CalculatedSteering(Agent agent, Vector3 desiredDirection)
    {
        Vector3 steering = desiredDirection - agent.CurrentVelocity;

        steering = Vector3.ClampMagnitude(steering, agent.MaxSteering * Time.deltaTime);

        return steering;
    }

    protected Vector3 CalculatedFuture(Agent agent, Agent target)
    {
        Vector3 direction = target.transform.position - agent.transform.position;
        float distance = direction.magnitude;

        float prediction = distance / (agent.CurrentSpeed + target.CurrentVelocity.magnitude);
        Vector3 futurePosition = target.transform.position + target.CurrentVelocity * prediction;
        return futurePosition;
    }
}
