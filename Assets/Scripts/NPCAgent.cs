using UnityEngine;

public enum hunterModes {Patrol, Bait, Attack, Gather}

public class NPCAgent : Agent
{
    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    private void Awake()
    {
        _stateMachine = new FSM();

        _patrolData._agent = this;

        PatrolState patrolState = new PatrolState(_patrolData, _stateMachine);

        _stateMachine.RegisterState(hunterModes.Patrol, patrolState);

        _stateMachine.StartFirstState(hunterModes.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();

        transform.position += GetCurrentVelocity() * Time.deltaTime;

        if (GetCurrentVelocity() != Vector3.zero)
        {
            transform.forward = GetCurrentVelocity();
        }

        transform.position = Bounds.Instance.OutOfBounds(transform.position);
    }
}
