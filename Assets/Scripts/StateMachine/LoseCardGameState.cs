using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCardGameState : CardGameState
{
    [SerializeField] Canvas _loseCanvas = null;
    [SerializeField] Canvas _gameCanvas = null;

    public override void Enter()
    {
        _gameCanvas.gameObject.SetActive(false);
        _loseCanvas.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        _loseCanvas.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        StateMachine.ChangeState<MenuCardGameState>();
    }
}
