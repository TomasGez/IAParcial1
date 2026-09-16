//using System.Collections.Generic;
//using UnityEditor.Rendering;
//using UnityEngine;

//public class AgentSteering : Agent
//{
//    [Header("References")]
//    [SerializeField] private Agent currentTarget;

//    [Header("Stats")]
//    [SerializeField] private float maxSpeed;
//    [SerializeField] private float maxSteering;
//    [SerializeField] private float minDistance;
//    [SerializeField] private float slowingDistance;

//    private static List<Agent> allAgents = new List<Agent>();
//    [Header("Flocking")]
//    [SerializeField] private float separationRadius;
//    [SerializeField] private float alignmentRadius;
//    [SerializeField] private float cohesionRadius;
//    [SerializeField, Range(0f, 1f)] private float separationWeight;
//    [SerializeField, Range(0f, 1f)] private float alignmentWeight;
//    [SerializeField, Range(0f, 1f)] private float cohesionWeight;

//    public enum steeringModes {Seek, Flee, Arrive, Pursuit, Evade, Flocking}
//    public steeringModes currentSteering;

//    private void Awake()
//    {
//        allAgents.Add(this);

//        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f,Random.Range(-1f, 1f));
//        GetCurrentVelocity() = randomDirection.normalized * maxSpeed;
//    }

//    private void Update()
//    {
//        SwitchSteering();

//        transform.position += GetCurrentVelocity() * Time.deltaTime;

//        if(GetCurrentVelocity() != Vector3.zero)
//        {
//            transform.forward = GetCurrentVelocity();
//        }

//        transform.position = Bounds.Instance.OutOfBounds(transform.position);
//    }

//    private void SwitchSteering()
//    {
//        switch (currentSteering)
//        {
//            case steeringModes.Seek:
//                Seek(currentTarget);
//                return;
//            case steeringModes.Flee:
//                Flee(currentTarget);
//                return;
//            case steeringModes.Arrive:
//                Arrive(currentTarget);
//                return;
//            case steeringModes.Pursuit:
//                Pursuit(currentTarget);
//                return;
//            case steeringModes.Evade:
//                Evade(currentTarget);
//                return;
//            case steeringModes.Flocking:
//                Flocking();
//                return;
//            default:
//                Flocking();
//                return;
//        }
//    }

//    private Vector3 CalculatedDesired(Vector3 targetPosition)
//    {
//        Vector3 desired = (targetPosition - transform.position).normalized;
//        desired *= maxSpeed;

//        return desired;
//    }

//    private Vector3 CalculatedSteering(Vector3 desiredVelocity)
//    {
//        Vector3 steering =  desiredVelocity - GetCurrentVelocity();

//        steering = Vector3.ClampMagnitude(steering, maxSteering * Time.deltaTime);

//        return steering;
//    }

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

//    //Metodo que hace lo mismo que un Vector3.Distance()
//    private bool InRange(Vector3 position, float radius)
//    {
//        return (position - transform.position).sqrMagnitude <= radius * radius; 
//    }

//    private Vector3 CalculatedSeparation(List<Agent> list, float radius)
//    {
//        Vector3 desiredVector = default;
//        int count = 0;

//        foreach(var item in list)
//        {
//            if(item == this) continue;

//            if(InRange(item.transform.position, separationRadius))
//            {
//                desiredVector += (item.transform.position - transform.position);
//                count++;
//            }
//        }

//        if(count == 0) return Vector3.zero;

//        desiredVector /= count;
//        return CalculatedSteering(-desiredVector.normalized * maxSpeed);
//    }

//    private Vector3 CalculatedAlignment(List<Agent> list, float radius)
//    {
//        Vector3 desiredVector = default;
//        int count = 0;

//        foreach(var item in list)
//        {
//            if(item == this) continue;

//            if(InRange(item.transform.position, alignmentRadius))
//            {
//                desiredVector += item.currentVelocity;
//                count++;
//            }
//        }

//        if(count == 0) return Vector3.zero;

//        desiredVector /= count;
//        return CalculatedSteering(desiredVector.normalized * maxSpeed);
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

//    private void Flocking()
//    {
//        currentVelocity += 
//        CalculatedSeparation(allAgents, separationRadius) * separationWeight + 
//        CalculatedAlignment(allAgents, alignmentRadius) * alignmentWeight + 
//        CalculatedCohesion(allAgents, cohesionRadius) * cohesionWeight;
//    }
//}
