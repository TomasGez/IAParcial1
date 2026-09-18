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

//    private void Evade(Agent target)
//    {
//        Vector3 desiredVelocity = CalculatedDesired(CalculatedFuture(target));

//        currentVelocity += CalculatedSteering(-desiredVelocity);
//    }
