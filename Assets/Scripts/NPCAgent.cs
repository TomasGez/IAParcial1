using UnityEngine;

public enum hunterModes {Patrol, Bait, Attack, Gather}

public class NPCAgent : Agent
{
    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    [Header("Bait Stats")]
    [SerializeField] private BaitData _baitData;

    [Header("Attack Stats")]
    [SerializeField] private AttackData _attackData;

    [Header("Gather Stats")]
    [SerializeField] private GatherData _gatherData;

    private void Awake()
    {
        _stateMachine = new FSM();

        PatrolState patrolState = new PatrolState(this, _patrolData, _stateMachine);
        BaitState baitState = new BaitState(this, _baitData, _stateMachine);
        AttackState attackState = new AttackState(_attackData, _stateMachine);
        GatherState gatherState = new GatherState(_gatherData, _stateMachine);

        _stateMachine.RegisterState(hunterModes.Patrol, patrolState);
        _stateMachine.RegisterState(hunterModes.Bait, baitState);
        _stateMachine.RegisterState(hunterModes.Attack, attackState);
        _stateMachine.RegisterState(hunterModes.Gather, gatherState);

        _stateMachine.StartFirstState(hunterModes.Patrol);

        currentSpeed = maxSpeed;
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void InstantiateBait(GameObject baitObject)
    {
        Instantiate(baitObject, transform.position, transform.rotation);
    }
}
