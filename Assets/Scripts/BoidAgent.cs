using UnityEngine;

public enum preyModes {Flock, Examine, Escape, Dead}

public class BoidAgent : Agent
{
    [Header("Flock Stats")]
    [SerializeField] private FlockData _flockData;

    [Header("Examine Stats")]
    [SerializeField] private ExamineData _examineData;

    [Header("Escape Stats")]
    [SerializeField] private EscapeData _escapeData;

    [Header("Dead Stats")]
    [SerializeField] private DeadData _deadData;

    private void Awake()
    {

        _stateMachine = new FSM();

        FlockState flockState = new FlockState(this, _flockData, _stateMachine);
        ExamineState examineState = new ExamineState(_examineData, _stateMachine);
        EscapeState escapeState = new EscapeState(_escapeData, _stateMachine);
        DeadState deadState = new DeadState(_deadData, _stateMachine);

        _stateMachine.RegisterState(preyModes.Flock, flockState);
        _stateMachine.RegisterState(preyModes.Examine, examineState);
        _stateMachine.RegisterState(preyModes.Escape, escapeState);
        _stateMachine.RegisterState(preyModes.Dead, deadState);

        Spawner.Instance.AddAgent(this);

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f,Random.Range(-1f, 1f));
        _currentVelocity = randomDirection.normalized * maxSpeed;
        
        _stateMachine.StartFirstState(preyModes.Flock);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }
}
