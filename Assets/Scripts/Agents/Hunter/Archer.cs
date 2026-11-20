using UnityEngine;

public class Archer : AgentHunter
{
    private HunterUI _hunterUI;
    private Animator _hunterAnim;

    public bool IsResting { get; private set; }

    [Header("Hunter Data")]
    [SerializeField] private float _maxEnergy;
    private float _currentEnergy;
    [SerializeField] private PatrolData _patrolData;
    [SerializeField] private BaitData _baitData;
    [SerializeField] private HuntData _huntData;
    [SerializeField] private HarvestData _harvestData;

    protected override void Awake()
    {
        base.Awake();

        _hunterUI = GetComponentInChildren<HunterUI>();
        _hunterAnim = GetComponentInChildren<Animator>();

        StatePatrol _statePatrol = new StatePatrol(this, _hunterUI, _hunterAnim, _patrolData, _stateMachine);
        StateBait _stateBait = new StateBait(this, _hunterUI, _hunterAnim, _baitData, _stateMachine);
        StateHunt _stateHunt = new StateHunt(this, _steering, _hunterUI, _hunterAnim, _huntData, _stateMachine);
        StateHarvest _stateHarvest = new StateHarvest(this, _steering, _hunterUI, _hunterAnim, _harvestData, _stateMachine);
        StateRest _stateRest = new StateRest(this, _hunterUI, _hunterAnim, _stateMachine);

        _stateMachine.RegisterState(HunterMode.Patrol, _statePatrol);
        _stateMachine.RegisterState(HunterMode.Bait, _stateBait);
        _stateMachine.RegisterState(HunterMode.Hunt, _stateHunt);
        _stateMachine.RegisterState(HunterMode.Harvest, _stateHarvest);
        _stateMachine.RegisterState(HunterMode.Rest, _stateRest);

        _currentEnergy = _maxEnergy;

        _stateMachine.StartFirstState(HunterMode.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void PlaceBait(Plant prefab, float condition)
    {
        Plant plant = Instantiate(prefab, transform.position, Quaternion.identity);

        Manager.Instance.AddPlant();

        plant.InitializeBait(condition);
    }

    public void StartHarvesting()
    {
        if (PreyTarget != null) _stateMachine.ChangeState(HunterMode.Harvest);
    }

    public void ConsumeEnergy()
    {
        _currentEnergy -= Time.deltaTime;

        _hunterUI.UpdateBottomBar(_currentEnergy, _maxEnergy);

        if (_currentEnergy <= 0)
        {
            IsResting = true;
            _currentEnergy = 0;
            _stateMachine.ChangeState(HunterMode.Rest);
            ClearPreys();
        }
    }

    public void RecoverEnergy()
    {
        _currentEnergy += Time.deltaTime * 2;

        _hunterUI.UpdateBottomBar(_currentEnergy, _maxEnergy);

        if (_currentEnergy >= _maxEnergy)
        {
            IsResting = false;
            _currentEnergy = _maxEnergy;
            _stateMachine.ChangeState(HunterMode.Patrol);
        }
    }
}
