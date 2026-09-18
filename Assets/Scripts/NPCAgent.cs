using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum hunterModes {Patrol, Bait, Attack, Gather}

public class NPCAgent : Agent
{
    private Queue<BoidAgent> targetQueue = new Queue<BoidAgent>();
    private bool isHunting;

    [Header("Patrol Stats")]
    [SerializeField] private PatrolData _patrolData;

    [Header("Bait Stats")]
    [SerializeField] private BaitData _baitData;

    [Header("Attack Stats")]
    [SerializeField] private AttackData _attackData;

    [Header("Gather Stats")]
    [SerializeField] private GatherData _gatherData;

    private void Awake()
    {
        _stateMachine = new FSM();

        PatrolState patrolState = new PatrolState(this, _patrolData, _stateMachine);
        BaitState baitState = new BaitState(this, _baitData, _stateMachine);
        AttackState attackState = new AttackState(this, _attackData, _stateMachine);
        GatherState gatherState = new GatherState(this, _gatherData, _stateMachine);

        _stateMachine.RegisterState(hunterModes.Patrol, patrolState);
        _stateMachine.RegisterState(hunterModes.Bait, baitState);
        _stateMachine.RegisterState(hunterModes.Attack, attackState);
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
        if (targetQueue != null && targetQueue.Count > 0)
        {
            return targetQueue.Peek();
        }
            return null;
    }

    public void RemoveCurrentBoidTarget()
    {
        targetQueue.Dequeue();
    }

    public void ClearTargetQueue()
    {
        targetQueue.Clear();
    }

    public bool GetIsHunting()
    {
        return isHunting;
    }

    public void SetIsHunting(bool value)
    {
        isHunting = value;
    }

    public void InstantiateBait(GameObject baitObject)
    {
        Instantiate(baitObject, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Prey"))
        {
            Debug.Log("Hunter following new prey");
            targetQueue.Enqueue(other.GetComponent<BoidAgent>());
            isHunting = true;
        }
    }
}
