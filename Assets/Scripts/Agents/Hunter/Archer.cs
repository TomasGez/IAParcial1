using UnityEngine;

public class Archer : AgentHunter
{
    private HunterUI _hunterUI;
    private Animator _hunterAnim;

    public bool IsRanged { get; private set; }
    public bool IsClose { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _hunterUI = GetComponentInChildren<HunterUI>();
        _hunterAnim = GetComponentInChildren<Animator>();

        StatePatrol _statePatrol = new StatePatrol(this, _hunterUI, _stateMachine);
        StateBait _stateBait = new StateBait(this, _hunterUI, _stateMachine);
        StateHunt _stateHunt = new StateHunt(this, _hunterUI, _stateMachine);
        StateHarvest _stateHarvest = new StateHarvest(this, _hunterUI, _stateMachine);
        StateRest _stateRest = new StateRest(this, _hunterUI, _stateMachine);

        _stateMachine.RegisterState(HunterMode.Patrol, _statePatrol);
        _stateMachine.RegisterState(HunterMode.Bait, _stateBait);
        _stateMachine.RegisterState(HunterMode.Hunt, _stateHunt);
        _stateMachine.RegisterState(HunterMode.Harvest, _stateHarvest);
        _stateMachine.RegisterState(HunterMode.Rest, _stateRest);

        _stateMachine.StartFirstState(HunterMode.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public void InShootRange(bool isActive)
    {
        if (IsRanged) return;

        IsRanged = isActive;

        if (IsRanged) _hunterUI.ChangeHuntIcon(_hunterUI.RangedTexture);

        else _hunterUI.ChangeHuntIcon(_hunterUI.FarTexture);
    }

    public void InMeleeRange(bool isActive)
    {
        if (IsClose) return;

        IsClose = isActive;

        if (IsClose) _hunterUI.ChangeHuntIcon(_hunterUI.MeleeTexture);

        else
        {
            if (IsRanged) _hunterUI.ChangeHuntIcon(_hunterUI.RangedTexture);

            else _hunterUI.ChangeHuntIcon(_hunterUI.FarTexture);
        }
    }
}
