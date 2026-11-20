using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected SteeringBehaviour _steering;


    [Header("Movement Stats")]
    [SerializeField] private float _maxSpeed;
    private float _currentSpeed;
    [SerializeField] private float _maxSteering;
    private Vector3 _currentVelocity;

    [Header("Sensor")]
    [SerializeField] private LayerMask _targetMask;

    public float MaxSpeed => _maxSpeed;
    public float CurrentSpeed => _currentSpeed;
    public float MaxSteering => _maxSteering;
    public Vector3 CurrentVelocity => _currentVelocity;
    public LayerMask TargetMask => _targetMask;

    protected virtual void Awake()
    {
        _stateMachine = new FSM();
        _steering = new SteeringBehaviour();

        _currentSpeed = _maxSpeed;
    }

    protected virtual void Movement()
    {
        transform.position += _currentVelocity * Time.deltaTime;

        if (_currentVelocity != Vector3.zero)
        {
            transform.forward = _currentVelocity;
        }
    }

    public void SetCurrentVelocity(Vector3 value)
    {
        _currentVelocity = value;
    }

    public void SetCurrentSpeed(float value)
    {
        _currentSpeed = value;
    }
}
