using System.Collections.Generic;
using UnityEngine;

public enum hunterModes {Patrol, Bait, Hunt, Gather}

public class NPCAgent : Agent
{
    public HunterUI hunterUI;
    private Queue<BoidAgent> _preyQueue = new Queue<BoidAgent>();
    [SerializeField] private float attackCooldown;
    public float _attackTimer;
    private bool _isHunting = false;

    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    [Header("Bait Stats")]
    [SerializeField] private BaitData _baitData;

    [Header("Attack Stats")]
    [SerializeField] private HuntData _huntData;

    [Header("Gather Stats")]
    [SerializeField] private GatherData _gatherData;

    private void Awake()
    {
        _stateMachine = new FSM();

        PatrolState patrolState = new PatrolState(this, _patrolData, _stateMachine);
        BaitState baitState = new BaitState(this, _baitData, _stateMachine);
        HuntState huntState = new HuntState(this, _huntData, _stateMachine);
        GatherState gatherState = new GatherState(this, _gatherData, _stateMachine);

        _stateMachine.RegisterState(hunterModes.Patrol, patrolState);
        _stateMachine.RegisterState(hunterModes.Bait, baitState);
        _stateMachine.RegisterState(hunterModes.Hunt, huntState);
        _stateMachine.RegisterState(hunterModes.Gather, gatherState);

        _stateMachine.StartFirstState(hunterModes.Patrol);

        _currentSpeed = maxSpeed;
    }

    private void Update()
    {
        _stateMachine.Update();

        Movement();
    }

    public BoidAgent GetCurrentBoidTarget()
    {
        if (_preyQueue != null && _preyQueue.Count > 0)
        {
            return _preyQueue.Peek();
        }
        else
        {
            return null;
        }
    }

    public void AddBoidTarget(BoidAgent target)
    {
        _preyQueue.Enqueue(target);
    }

    public void RemoveCurrentBoidTarget()
    {
        _preyQueue.Dequeue();
    }

    public void ClearTargetQueue()
    {
        _preyQueue.Clear();
    }

    public float GetAttackCooldown()
    {
        return attackCooldown;
    }

    public float GetAttackTimer()
    {
        return _attackTimer;
    }

    public void Reloading()
    {
        if(_attackTimer < attackCooldown)
        {
            _attackTimer += Time.deltaTime;
            hunterUI.ReloadAttack(_attackTimer, attackCooldown);
        }
    }

    public bool GetIsHunting()
    {
        return _isHunting;
    }

    public void SetIsHunting(bool value)
    {
        _isHunting = value;
    }

    public void InstantiateBait(GameObject baitObject)
    {
        Instantiate(baitObject, transform.position, transform.rotation);
    }
}
