using UnityEngine;

public enum preyModes {Flock, Examine, Escape, Dead}

public class BoidAgent : Agent
{
    private void Awake()
    {
        _stateMachine = new FSM();
    }

    private void Update()
    {
        
    }
}
