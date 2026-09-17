using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected Vector3 _currentVelocity;

    [Header("Agents Stats")]
    [SerializeField] protected float maxSpeed;
    protected float currentSpeed;
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
        return currentSpeed;
    }

    public void SetCurrentSpeed(float value)
    {
        currentSpeed = value;
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
        direction *= currentSpeed;

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
