using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected Vector3 _currentVelocity;

    [Header("Agents Stats")]
    [SerializeField] protected float maxSpeed;
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

    public void Movement()
    {
        transform.position += GetCurrentVelocity() * Time.deltaTime;

        if (GetCurrentVelocity() != Vector3.zero)
        {
            transform.forward = GetCurrentVelocity();
        }

        transform.position = Bounds.Instance.OutOfBounds(transform.position);
    }

    public Vector3 CalculatedDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction *= maxSpeed;

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
}
