//using System.Collections.Generic;
//using UnityEditor.Rendering;
//using UnityEngine;

//public class AgentSteering : Agent
//{
//    [Header("References")]
//    [SerializeField] private Agent currentTarget;

//    [Header("Stats")]
//    [SerializeField] private float minDistance;
//    [SerializeField] private float slowingDistance;

//    private void Seek(Agent target)
//    {
//        Vector3 desiredVelocity = CalculatedDesired(target.transform.position);

//        GetCurrentVelocity() += CalculatedSteering(desiredVelocity);
//    }

//    private void Flee(Agent target)
//    {
//        Vector3 desiredVelocity = CalculatedDesired(target.transform.position);

//        currentVelocity += CalculatedSteering(-desiredVelocity);
//    }

//    private void Arrive(Agent target)
//    {
//        Vector3 direction = target.transform.position - transform.position;
//        float distance = direction.magnitude;

//        if(distance < minDistance)
//        {
//            currentVelocity = Vector3.zero;
//            return;
//        }

//        float targetSpeed = maxSpeed * (distance / slowingDistance);
//        float desiredSpeed = Mathf.Min(targetSpeed, maxSpeed);

//        Vector3 desiredVelocity = direction.normalized * desiredSpeed;

//        currentVelocity += CalculatedSteering(desiredVelocity);
//    }

//    private Vector3 CalculatedFuture(Agent target)
//    {
//        Vector3 direction = target.transform.position - transform.position;
//        float distance = direction.magnitude;

//        float prediction = distance / (maxSpeed + target.currentVelocity.magnitude);
//        Vector3 futurePosition = target.transform.position + target.currentVelocity * prediction;
//        return futurePosition;
//    }

//    private void Pursuit(Agent target)
//    {
//        Vector3 desiredVelocity = CalculatedDesired(CalculatedFuture(target));

//        currentVelocity += CalculatedSteering(desiredVelocity);
//    }

//    private void Evade(Agent target)
//    {
//        Vector3 desiredVelocity = CalculatedDesired(CalculatedFuture(target));

//        currentVelocity += CalculatedSteering(-desiredVelocity);
//    }


//    private Vector3 CalculatedCohesion(List<Agent> list, float radius)
//    {
//        Vector3 desiredVector = default;
//        int count = 0;

//        foreach(var item in list)
//        {
//            if(item == this) continue;

//            if(InRange(item.transform.position, cohesionRadius))
//            {
//                desiredVector += item.transform.position;
//                count++;
//            }
//        }

//        if(count == 0) return Vector3.zero;

//        desiredVector /= count;

//        Vector3 desiredVelocity = CalculatedDesired(desiredVector);
//        return CalculatedSteering(desiredVelocity.normalized * maxSpeed);
//    }
