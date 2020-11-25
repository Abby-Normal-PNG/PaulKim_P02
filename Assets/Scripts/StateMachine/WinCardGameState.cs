using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCardGameState : CardGameState
{
    [SerializeField] Canvas _winCanvas = null;
    [SerializeField] Canvas _gameCanvas = null;
    [SerializeField] CanvasGroup _persistentCG;

    public override void Enter()
    {
        Debug.Log("Win State: Entering...");
        _gameCanvas.gameObject.SetActive(false);
        _winCanvas.gameObject.SetActive(true);
        _persistentCG.alpha = 0;

        StateMachine.BGM.PlayVictoryBGM();
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
