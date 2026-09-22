using UnityEngine;

public class BaitState : State
{
    public BaitState (NPCAgent agent, HunterUI UI, BaitData data, FSM stateMachine) : base(stateMachine)
    {
        _UI = UI;
        _agent = agent;
        _baitData = data;
    }

    private NPCAgent _agent;
    private HunterUI _UI;
    private BaitData _baitData;
    private float _stopingVelocity;
    private float _placeTimer = 0;

    public override void Enter()
    {
        _placeTimer = 0;
        _stopingVelocity = _baitData.stopStart;

        _UI.PlacingBait(_placeTimer, _baitData.placeBaitCooldown);
        _UI.ChangeHunterUI(hunterModes.Bait);
    }

    public override void Update()
    {
        _UI.PlacingBait(_placeTimer, _baitData.placeBaitCooldown);

        _stopingVelocity -= Time.deltaTime;
        _agent.SetCurrentVelocity(_agent.GetCurrentVelocity() * Mathf.Clamp(_stopingVelocity, 0f, _baitData.stopStart));

        if(_stopingVelocity <= 0)
        {
            _placeTimer += Time.deltaTime;

            if(_placeTimer > _baitData.placeBaitCooldown)
            {
                _agent.InstantiateBait(_baitData.baitPrefab);
                _stateMachine.ChangeState(hunterModes.Patrol);
            }
        }
    }

    public override void Exit()
    {
        _agent.SetCurrentSpeed(_agent.GetMaxSpeed());
    }
}

[System.Serializable]
public class BaitData
{
    public GameObject baitPrefab;
    public float stopStart;
    public float placeBaitCooldown;
}