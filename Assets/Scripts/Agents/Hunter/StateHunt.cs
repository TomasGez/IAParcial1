using Unity.VisualScripting;
using UnityEngine;

public class StateHunt : State
{
    public StateHunt(Archer agent, SteeringBehaviour steering, HunterUI UI, Animator anim, HuntData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _steering = steering;
        _UI = UI;
        _anim = anim;
        _data = data;
    }

    private Archer _agent;
    private SteeringBehaviour _steering;
    private HunterUI _UI;
    private Animator _anim;
    private HuntData _data;

    private enum RangeState { Melee, Ranged, OutOfRange }
    private float _timer;

    public override void Enter()
    {
        _UI.ChangeMode(HunterMode.Hunt);
        _anim.SetTrigger("IsWalking");

        _timer = 0;
        _UI.UpdateBar(_timer, _data.huntTime, HunterMode.Hunt);
        _agent.SetCurrentSpeed(_agent.MaxSpeed);
    }

    public override void Update()
    {
        if (_agent.PreyTarget == null)
        {
            _stateMachine.ChangeState(HunterMode.Patrol);
            return;
        }

        if (_agent.PreyTarget != null && _agent.PreyTarget.IsDead)
        {
            _stateMachine.ChangeState(HunterMode.Harvest);
            return;
        }

        if (_agent.PreyTarget != null) Attack();

        _agent.ConsumeEnergy();

        if (_agent.PreyTarget != null) Pursuit();
    }

    public override void Exit()
    {
        _timer = 0;

        _anim.ResetTrigger("IsWalking");
    }

    private void Pursuit()
    {
        Vector3 desiredVelocity = _steering.CalculatedDirection(_agent, _steering.CalculatedFuture(_agent, _agent.PreyTarget));

        _agent.SetCurrentVelocity(_agent.CurrentVelocity + _steering.CalculatedSteering(_agent, desiredVelocity));
    }

    private void Attack()
    {
        float sqrDist = (_agent.PreyTarget.transform.position - _agent.transform.position).sqrMagnitude;

        bool inMelee = sqrDist <= _data.meleeDistance * _data.meleeDistance;
        bool inRanged = !inMelee && sqrDist <= _data.rangedDistance * _data.rangedDistance;

        _timer = Mathf.Min(_timer + Time.deltaTime, _data.huntTime);
        _UI.UpdateBar(_timer, _data.huntTime, HunterMode.Hunt);

        Sprite icon = inMelee ? _UI.MeleeTexture : inRanged ? _UI.RangedTexture : _UI.FarTexture;
        _UI.ChangeHuntIcon(icon);

        if (!inMelee && !inRanged) return;

        if (_timer >= _data.huntTime)
        {
            _timer = 0;
            _agent.PreyTarget.GetHit(inMelee ? _data.meleeDamage : _data.rangedDamage);
        }
    }
}

[System.Serializable]
public class HuntData
{
    public float huntTime;
    public float rangedDistance;
    public float meleeDistance;
    public int rangedDamage;
    public int meleeDamage;
}