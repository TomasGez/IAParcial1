using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected Vector3 _currentVelocity;

    [Header("Agent Stats")]
    [SerializeField] protected float maxSpeed;
    protected float _currentSpeed;
    [SerializeField] protected float maxSteering;

    public Vector3 GetCurrentVelocity()
    {
        return _currentVelocity;
    }

    public void SetCurrentVelocity(Vector3 value)
    {
        _currentVelocity = value;
    }

    public void AddToCurrentVelocity(Vector3 value)
    {
        _currentVelocity += value;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public float GetCurrentSpeed()
    {
        return _currentSpeed;
    }

    public void SetCurrentSpeed(float value)
    {
        _currentSpeed = value;
    }

    public void Movement()
    {
        transform.position += _currentVelocity * Time.deltaTime;

        if (_currentVelocity != Vector3.zero)
        {
            transform.forward = _currentVelocity;
        }

        transform.position = Bounds.Instance.OutOfBounds(transform.position);
    }

    public Vector3 CalculatedDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction *= _currentSpeed;

        return direction;
    }

    public Vector3 CalculatedSteering(Vector3 desiredDirection)
    {
        Vector3 steering =  desiredDirection - _currentVelocity;

        steering = Vector3.ClampMagnitude(steering, maxSteering * Time.deltaTime);

        return steering;
    }

    public void Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = CalculatedDirection(targetPosition);

        _currentVelocity += CalculatedSteering(desiredVelocity);
    }

    public Vector3 CalculatedFuture(Agent target)
    {
        Vector3 direction = target.transform.position - transform.position;
        float distance = direction.magnitude;

        float prediction = distance / (maxSpeed + target._currentVelocity.magnitude);
        Vector3 futurePosition = target.transform.position + target._currentVelocity * prediction;
        return futurePosition;
    }

    public void Pursuit(Agent target, float slowingDistance, float minDistance)
    {
        Vector3 desiredVelocity = CalculatedDirection(CalculatedFuture(target));

        _currentVelocity += CalculatedSteering(desiredVelocity);

        float distance = Vector3.Distance(target.transform.position, transform.position);

        if(distance <= minDistance)
        {
            _currentVelocity = Vector3.zero;
            return;
        }

        float stoppingDistance = distance - minDistance;
        float targetSpeed = maxSpeed * (stoppingDistance / slowingDistance);
        _currentSpeed = Mathf.Min(targetSpeed, maxSpeed);
    }
}
