using UnityEngine;

public class StateRespawn : State
{
    public StateRespawn(Animal agent, RespawnData data, FSM stateMachine) : base(stateMachine)
    {
        _agent = agent;
        _data = data;
    }

    private Animal _agent;
    private RespawnData _data;

    private float _timer;

    public override void Enter()
    {
        _timer = 0;

        DeactivateAgent(true);
    }

    public override void Update()
    {
        if (_timer < _data.respawnTime)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _data.respawnTime)
        {
            DeactivateAgent(false);
            _stateMachine.ChangeState(PreyMode.Flock);
        }
    }

    public override void Exit()
    {
        _timer = 0;
    }

    private void DeactivateAgent(bool isActive)
    {
        foreach (Transform child in _agent.transform)
        {
            child.gameObject.SetActive(!isActive);
        }

        _agent._preyCollider.enabled = !isActive;
        _agent._preyRB.detectCollisions = !isActive;
    }
}

[System.Serializable]
public class RespawnData
{
    public float respawnTime;
}
