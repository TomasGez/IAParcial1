using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    private Vector3 _currentVelocity;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxSteering;

    public Vector3 GetCurrentVelocity()
    {
        return _currentVelocity;
    }

    public void SetCurrentVelocity(Vector3 value)
    {
        _currentVelocity = value;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
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
