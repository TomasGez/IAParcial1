using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum hunterModes {Patrol, Bait, Hunt, Gather}

public class NPCAgent : Agent
{
    private Queue<BoidAgent> preyQueue = new Queue<BoidAgent>();
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
        if (preyQueue != null && preyQueue.Count > 0)
        {
            return preyQueue.Peek();
        }
            return null;
    }

    public void AddBoidTarget(BoidAgent target)
    {
        preyQueue.Enqueue(target);
    }

    public void RemoveCurrentBoidTarget()
    {
        preyQueue.Dequeue();
    }

    public void ClearTargetQueue()
    {
        preyQueue.Clear();
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
