using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCardGameState : CardGameState
{
    [SerializeField] Canvas _menuCanvas;
    [SerializeField] Date _date;

    public override void Enter()
    {
        _menuCanvas.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        _menuCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        StateMachine.ChangeState<SetupCardGameState>();
    }
}
