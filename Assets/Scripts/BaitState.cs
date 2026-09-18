using UnityEngine;

public class BaitState : State
{
    public BaitState (NPCAgent agent, BaitData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _baitData = data;
    }

    private NPCAgent _agent;
    private BaitData _baitData;
    private float _stopingVelocity;
    private float _placeTimer;

    public override void Enter()
    {
        _placeTimer = _baitData.placeBaitCooldown;
        _stopingVelocity = _baitData.stopStart;
    }

    public override void Update()
    {
        _stopingVelocity -= Time.deltaTime;
        _agent.SetCurrentVelocity(_agent.GetCurrentVelocity() * Mathf.Clamp(_stopingVelocity, 0f, _baitData.stopStart));

        if(_stopingVelocity <= 0)
        {
            _placeTimer -= Time.deltaTime;

            if(_placeTimer <= 0)
            {
                _agent.InstantiateBait(_baitData.baitPrefab);
                _stateMachine.ChangeState(hunterModes.Patrol);
            }
        }
    }

    public override void Exit()
    {
        _placeTimer = _baitData.placeBaitCooldown;
        _stopingVelocity = _baitData.stopStart;
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