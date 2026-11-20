using UnityEngine;

public class StateBait : State
{
    public StateBait(Archer agent, HunterUI UI, Animator anim, BaitData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Archer _agent;
    private HunterUI _UI;
    private Animator _anim;
    private BaitData _data;

    private float _timer;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Bait);
        _anim.SetTrigger("IsIdle");

        _timer = 0;
        _UI.UpdateBar(_timer, _data.baitTime, HunterMode.Bait);
        _agent.SetCurrentVelocity(Vector3.zero);
    }

    public override void Update()
    {
        if (_timer < _data.baitTime)
        {
            _timer += Time.deltaTime;
            _UI.UpdateBar(_timer, _data.baitTime, HunterMode.Bait);
        }

        if (_timer >= _data.baitTime)
        {
            if (Manager.Instance.GetTotalPlants() < _data.totalBaits)
            {
                _agent.PlaceBait(_data.baitPrefab, _data.baitCondition);
                _stateMachine.ChangeState(HunterMode.Patrol);
                return;
            }
        }
    }

    public override void Exit()
    {
        _timer = 0;

        _anim.ResetTrigger("IsIdle");
    }
}

[System.Serializable]
public class BaitData
{
    public Plant baitPrefab;
    public float baitCondition;
    public float baitTime;
    public int totalBaits;
}