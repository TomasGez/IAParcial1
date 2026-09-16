using UnityEngine;

public enum hunterModes {Patrol, Bait, Attack, Gather}

public class AgentNPC : Agent
{
    private void Awake()
    {
        _stateMachine = new FSM();
    }

    private void Update()
    {
        
    }
}
