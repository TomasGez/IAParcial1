using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public State _currentState {get; private set;}
    private Dictionary<Enum, State> states = new Dictionary<Enum, State>();

    public void RegisterState(Enum key, State state)
    {
        states[key] = state;
    }

    public void StartFirstState(Enum key)
    {
        _currentState = states[key];
        _currentState?.Enter();
    }

    public void ChangeState(Enum key)
    {
        State newState = states[key];

        if(newState == _currentState) return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }
}
