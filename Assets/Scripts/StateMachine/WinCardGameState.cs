using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCardGameState : CardGameState
{
    [SerializeField] Canvas _winCanvas = null;
    [SerializeField] Canvas _gameCanvas = null;

    public override void Enter()
    {
        _gameCanvas.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        _winCanvas.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        StateMachine.ChangeState<MenuCardGameState>();
    }
}
