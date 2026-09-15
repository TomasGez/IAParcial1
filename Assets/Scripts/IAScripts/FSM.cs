using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public State currentState {get; private set;}
    private Dictionary<Enum, State> states = new Dictionary<Enum, State>();

    public void RegisterState(Enum key, State state)
    {
        states[key] = state;
    }

    public void ChangeState(Enum key)
    {
        State newState = states[key];

        if(newState == currentState) return;

        //Se agrega un signo de pregunta para no ejecutar la funcion si es nula
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}
