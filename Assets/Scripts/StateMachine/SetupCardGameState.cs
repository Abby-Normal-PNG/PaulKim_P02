using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupCardGameState : CardGameState
{
    [SerializeField] Canvas _gameplayCanvas = null;
    [SerializeField] PlayerDeckManager _playerDeck;
    [SerializeField] DateDeckManager _dateDeck;
    [SerializeField] CardGameUIController _ui;
    [SerializeField] Date _date;

    bool _activated = false;
    public bool _shouldResetDate = false;

    public override void Enter()
    {
        Debug.Log("Setup: Entering...");

        _gameplayCanvas.gameObject.SetActive(true);

        StateMachine.BGM.PlayGameplayBGM();

        _playerDeck.ClearDecks();
        _dateDeck.ClearDecks();
        _playerDeck.SetupDrawDeck();
        _dateDeck.SetupDrawDeck();

        if (_shouldResetDate)
        {
            _date.ResetMods();
            _date.ResetStats();
            _shouldResetDate = false;
        }
        
        _ui.UpdateJoySlider();
        _ui.UpdateLoveSlider();
        _ui.UpdatePatienceSlider();
        _ui.OnEnemyTurnEnded();
        _playerDeck.UpdateHandVisuals();

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
