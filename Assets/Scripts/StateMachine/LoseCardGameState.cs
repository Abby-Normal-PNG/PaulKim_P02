using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCardGameState : CardGameState
{
    [SerializeField] Canvas _loseCanvas = null;
    [SerializeField] Canvas _gameCanvas = null;
    [SerializeField] CanvasGroup _persistantCG = null;
    [SerializeField] BoardManager _board;
    [SerializeField] PlayerRecords _records;

    public override void Enter()
    {
        //_gameCanvas.gameObject.SetActive(false);
        _loseCanvas.gameObject.SetActive(true);
        _persistantCG.alpha = 0;

        _board.ClearBoard();

        _records.Lose();

        StateMachine.BGM.PlayWaitingBGM();
    }

    public override void Exit()
    {
        _loseCanvas.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        PlayerTurnCardGameState._playerTurnCount = 0;
        StateMachine.ChangeState<MenuCardGameState>();
    }
}
