using UnityEngine;

public enum preyModes {Flock, Examine, Escape, Dead}

public class BoidAgent : Agent
{
    [SerializeField] private PreyUI preyUI;
    private InterestItem _bait;
    private NPCAgent _hunter;
    private bool _isExamining = false;
    private bool _isEscaping = false;
    private bool _isDead = false;

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

        FlockState flockState = new FlockState(this, preyUI, _flockData, _stateMachine);
        ExamineState examineState = new ExamineState(this, preyUI, _examineData, _stateMachine);
        EscapeState escapeState = new EscapeState(this, preyUI, _escapeData, _stateMachine);
        DeadState deadState = new DeadState(this, preyUI, _deadData, _stateMachine);

        _stateMachine.RegisterState(preyModes.Flock, flockState);
        _stateMachine.RegisterState(preyModes.Examine, examineState);
        _stateMachine.RegisterState(preyModes.Escape, escapeState);
        _stateMachine.RegisterState(preyModes.Dead, deadState);

        Spawner.Instance.AddAgent(this);

        _currentSpeed = maxSpeed;

        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f,Random.Range(-1f, 1f));
        _currentVelocity = randomDirection.normalized * _currentSpeed;
        
        _stateMachine.StartFirstState(preyModes.Flock);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public InterestItem GetCurrentItem()
    {
        return _bait;
    }

    public void SetCurrentItem(InterestItem target)
    {
        _bait = target;
    }
    public NPCAgent GetNPCTarget()
    {
        return _hunter;
    }

    public void SetNPCTarget(NPCAgent target)
    {
        _hunter = target;
    }

    public bool GetIsExamining()
    {
        return _isExamining;
    }

    public void SetIsExamining(bool value)
    {
        _isExamining = value;
    }
    public bool GetIsEscaping()
    {
        return _isEscaping;
    }

    public void SetIsEscaping(bool value)
    {
        _isEscaping = value;
    }

    public bool GetIsDead()
    {
        return _isDead;
    }

    public void SetIsDead(bool value)
    {
        _isDead = value;
    }

    public void TerminatePrey()
    {
        Destroy(gameObject);
    }
}
