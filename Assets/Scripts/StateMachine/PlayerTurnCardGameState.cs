using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{
    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] PlayerDeckManager _playerDeck = null;
    [SerializeField] CanvasGroup _turnCanvas = null;
    [SerializeField] TweenThis _playerTurnTextTween = null;
    [SerializeField] BoardManager _board = null;
    [SerializeField] Date _date = null;

    [SerializeField] int _turnsPerRound = 5;
    public int TurnsPerRound => _turnsPerRound;

    public static int _playerTurnCount = 0;
    public static int PlayerTurnCount => _playerTurnCount;

    public override void Enter()
    {
        Debug.Log("Player Turn: Entering...");
        _playerTurnTextUI.gameObject.SetActive(true);
        _playerTurnTextTween.PopIn() ;
        _turnCanvas.gameObject.SetActive(true);
        _turnCanvas.blocksRaycasts = true;
        _turnCanvas.interactable = true;

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString() + "/" + _turnsPerRound.ToString();
        
        if(_playerTurnCount == 1)
        {
            _date.ResetMods();
            _board.ClearBoard();
            _playerDeck.DrawStartingHand();
        }

        //Allow player to draw
        _playerDeck._canDraw = true;
        _playerDeck.CheckPassPlayState();
        //Make sure player hand updates visuals
        _playerDeck.UpdateHandVisuals();

        //hook into events
        StateMachine.Input.PlayerTurnEnd += OnPlayerTurnEnd;
        StateMachine.Input.PressedPass += OnPlayerTurnPass;
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        _playerTurnTextTween.PopOut();
        _turnCanvas.gameObject.SetActive(false);
        _turnCanvas.blocksRaycasts = false;
        _turnCanvas.interactable = false;
        //unhook events
        StateMachine.Input.PlayerTurnEnd -= OnPlayerTurnEnd;
        StateMachine.Input.PressedPass -= OnPlayerTurnPass;
        Debug.Log("Player Turn: Exiting...");
    }

    private void OnPlayerTurnEnd()
    {
        StateMachine.ChangeState<PlayerPlaceCardGameState>();
    }

    private void OnPlayerTurnPass()
    {
        StateMachine.ChangeState<DateTurnCardGameState>();
    }

    public static void ResetTurnCount()
    {
        _playerTurnCount = 0;
    }

    public void ReturnToMenu()
    {
        PlayerTurnCardGameState._playerTurnCount = 0;
        StateMachine.ChangeState<MenuCardGameState>();
    }
}
