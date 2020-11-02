using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] PlayerConvoDeckManager _playerDeck = null;

    [SerializeField] static int _turnsPerRound = 5;
    public static int TurnsPerRound => _turnsPerRound;

    static int _playerTurnCount = 0;
    public static int PlayerTurnCount => _playerTurnCount;

    public override void Enter()
    {
        Debug.Log("Player Turn: Entering...");
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();
        


        //Allow player to draw
        _playerDeck._canDraw = true;

        //hook into events
        StateMachine.Input.PlayerTurnEnd += OnPlayerTurnEnd;
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        //unhook events
        StateMachine.Input.PlayerTurnEnd -= OnPlayerTurnEnd;
        Debug.Log("Player Turn: Exiting...");
    }

    private void OnPlayerTurnEnd()
    {
        StateMachine.ChangeState<DateTurnCardGameState>();
    }

    public static void ResetTurnCount()
    {
        _playerTurnCount = 0;
    }
}
