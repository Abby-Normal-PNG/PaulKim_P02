using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupCardGameState : CardGameState
{
    [SerializeField] Canvas _gameplayCanvas = null;

    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup: Entering...");

        _gameplayCanvas.gameObject.SetActive(true);

        _activated = false;
    }

    public override void Tick()
    {
        if(_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }
}
