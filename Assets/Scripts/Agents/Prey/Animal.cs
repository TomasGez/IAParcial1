using UnityEngine;

public class Animal : AgentPrey
{
    private PreyUI _preyUI;
    private Animator _preyAnim;

    public bool IsDead { get; private set; }
    public Collider _preyCollider { get; private set; }
    public Rigidbody _preyRB { get; private set; }

    [Header("Prey Data")]
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField] private FlockData _flockData;
    [SerializeField] private ConsumeData _consumeData;
    [SerializeField] private RespawnData _respawnData;

    protected override void Awake()
    {
        base.Awake();

        _preyUI = GetComponentInChildren<PreyUI>();
        _preyAnim = GetComponentInChildren<Animator>();

        _preyCollider = GetComponent<Collider>();
        _preyRB = GetComponent<Rigidbody>();

        StateFlock _stateFlock = new StateFlock(this, _steering, _preyUI, _preyAnim, _flockData, _stateMachine);
        StateExamine _stateExamine = new StateExamine(this, _steering, _preyUI, _preyAnim, _stateMachine);
        StateConsume _stateConsume = new StateConsume(this, _steering, _preyUI, _preyAnim, _consumeData, _stateMachine);
        StateEscape _stateEscape = new StateEscape(this, _steering, _preyUI, _preyAnim, _stateMachine);
        StateDead _stateDead = new StateDead(this, _preyUI, _preyAnim, _stateMachine);
        StateRespawn _stateRespawn = new StateRespawn(this, _respawnData, _stateMachine);

        _stateMachine.RegisterState(PreyMode.Flock, _stateFlock);
        _stateMachine.RegisterState(PreyMode.Examine, _stateExamine);
        _stateMachine.RegisterState(PreyMode.Consume, _stateConsume);
        _stateMachine.RegisterState(PreyMode.Escape, _stateEscape);
        _stateMachine.RegisterState(PreyMode.Dead, _stateDead);
        _stateMachine.RegisterState(PreyMode.Respawn, _stateRespawn);

        _currentHealth = _maxHealth;

        _stateMachine.StartFirstState(PreyMode.Flock);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void StartConsuming()
    {
        if (BaitTarget != null && HunterTarget == null) _stateMachine.ChangeState(PreyMode.Consume);
    }

    public void GetHit(int value)
    {
        _currentHealth -= value;

        _preyUI.UpdateBottomBar(_currentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        IsDead = true;

        ClearPreys(this);
        ClearBaits();
        HunterExited(HunterTarget);

        _stateMachine.ChangeState(PreyMode.Dead);
    }

    public void TerminateAnimal()
    {
        IsDead = false;

        _stateMachine.ChangeState(PreyMode.Respawn);
    }
}
