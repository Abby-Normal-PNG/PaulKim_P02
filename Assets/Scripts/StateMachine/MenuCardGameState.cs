using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCardGameState : CardGameState
{
    [SerializeField] Canvas _menuCanvas;
    [SerializeField] CanvasGroup _persistentCG;
    [SerializeField] SetupCardGameState _setup;

    public override void Enter()
    {
        _menuCanvas.gameObject.SetActive(true);
        _setup._shouldResetDate = true;

        StateMachine.BGM.PlayMenuBGM();
    }

    public override void Exit()
    {
        _menuCanvas.gameObject.SetActive(false);
        _persistentCG.alpha = 1;
    }

    public void StartGame()
    {
        StateMachine.ChangeState<SetupCardGameState>();
    }
}
