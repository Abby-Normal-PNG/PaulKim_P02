﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndCardGameState : CardGameState
{
    [SerializeField] Canvas _roundEndCanvas = null;
    [SerializeField] Canvas _gameCanvas = null;

    public override void Enter()
    {
        _gameCanvas.gameObject.SetActive(false);
        _roundEndCanvas.gameObject.SetActive(true);

        StateMachine.BGM.PlayWaitingBGM();
    }

    public override void Exit()
    {
        _roundEndCanvas.gameObject.SetActive(false);
    }

    public void StartNextRound()
    {
        PlayerTurnCardGameState._playerTurnCount = 0;
        StateMachine.ChangeState<SetupCardGameState>();
    }
}
