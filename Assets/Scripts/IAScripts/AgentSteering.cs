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

//        float targetSpeed = maxSpeed * (distance / slowingDistance);
//        float desiredSpeed = Mathf.Min(targetSpeed, maxSpeed);

//        Vector3 desiredVelocity = direction.normalized * desiredSpeed;

//        currentVelocity += CalculatedSteering(desiredVelocity);
//    }

//    private void Evade(Agent target)
//    {
//        Vector3 desiredVelocity = CalculatedDesired(CalculatedFuture(target));

//        currentVelocity += CalculatedSteering(-desiredVelocity);
//    }
