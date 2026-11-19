using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected SteeringBehaviour _steering;
    private Coroutine _keepLookingRoutine;


    [Header("Movement Stats")]
    protected Vector3 _currentVelocity;
    [SerializeField] protected float _maxSpeed;
    protected float _currentSpeed;
    [SerializeField] protected float _maxSteering;

    public Vector3 CurrentVelocity => _currentVelocity;
    public float CurrentSpeed => _currentSpeed;
    public float MaxSteering => _maxSteering;

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


}
