using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    State _currentState;
    public State CurrentState => _currentState;

    protected bool InTransition { get; private set; }
    protected State _previousState;

    private void Update()
    {
        if(CurrentState != null && !InTransition)
        {
            CurrentState.Tick();
        }
    }

    public void ChangeState<T>() where T : State
    {
        T targetState = GetComponent<T>();
        if(targetState == null)
        {
            Debug.LogWarning("Cannot change state, as it" +
                "does not exist on the State Machine " +
                "Object. Make sure ou have the desired State" +
                "attacjed to the State Machine!");
            return;
        }
        InitiateStateChange(targetState);
    }

    public void RevertState()
    {
        if(_previousState != null)
        {
            InitiateStateChange(_previousState);
        }
        else
        {
            Debug.LogWarning("Previous state does not " +
                "exist!");
        }
    }

    void InitiateStateChange(State targetState)
    {
        if(_currentState != targetState && !InTransition)
        {
            Transition(targetState);
        }
    }

    private void Transition(State newState)
    {
        InTransition = true;
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
        InTransition = false;
    }
}
