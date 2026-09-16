using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    protected FSM _stateMachine;
    protected Vector3 _currentVelocity;
    protected float maxSpeed;

    //eliminate 
    public Vector3 currentVelocity;
}
