using UnityEngine;

public class Animal : AgentPrey
{
    private PreySensor _preySensor;
    private PreyUI _preyUI;
    private Animator _preyAnim;

    public bool IsDead { get; private set; }

    [Header("Prey Data")]
    [SerializeField] private FlockData _flockData;
    [SerializeField] private ConsumeData _consumeData;

    protected override void Awake()
    {
        base.Awake();

        _preySensor = GetComponentInChildren<PreySensor>();
        _preyUI = GetComponentInChildren<PreyUI>();
        _preyAnim = GetComponentInChildren<Animator>();

        StateFlock _stateFlock = new StateFlock(this, _steering, _preyUI, _preyAnim, _flockData, _stateMachine);
        StateExamine _stateExamine = new StateExamine(this, _steering, _preyUI, _preyAnim, _stateMachine);
        StateConsume _stateConsume = new StateConsume(this, _steering, _preyUI, _preyAnim, _consumeData, _stateMachine);
        StateEscape _stateEscape = new StateEscape(this, _steering, _preyUI, _preyAnim, _stateMachine);
        StateDead _stateDead = new StateDead(this, _preyUI, _stateMachine);
        StateRespawn _stateRespawn = new StateRespawn(this, _preyUI, _stateMachine);

        _stateMachine.RegisterState(PreyMode.Flock, _stateFlock);
        _stateMachine.RegisterState(PreyMode.Examine, _stateExamine);
        _stateMachine.RegisterState(PreyMode.Consume, _stateConsume);
        _stateMachine.RegisterState(PreyMode.Escape, _stateEscape);
        _stateMachine.RegisterState(PreyMode.Dead, _stateDead);
        _stateMachine.RegisterState(PreyMode.Respawn, _stateRespawn);

        _stateMachine.StartFirstState(PreyMode.Flock);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void StartConsuming()
    {
        if (BaitTarget != null) _stateMachine.ChangeState(PreyMode.Consume);
    }

    public void SetIsDead(bool isActive)
    {
        IsDead = isActive;

        if (IsDead)
        {
            ClearPreys(this);
            ClearBaits();
            HunterExited(HunterTarget);
            _stateMachine.ChangeState(PreyMode.Dead);
        }
    }
}
