using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateTurnCardGameState : CardGameState
{
    public static event Action DateTurnBegan;
    public static event Action DateTurnEnded;

    [SerializeField] float _pauseDuration = 1.5f;
    [SerializeField] Date _date;
    [SerializeField] DateDeckManager _dateDeck;
    [SerializeField] PlayerTurnCardGameState _playerTurn = null;

    private Coroutine _coroutine;
    private bool _dateThinking = false;

    public override void Enter()
    {
        Debug.Log("Date Turn: Entering...");
        DateTurnBegan?.Invoke();

        //If first turn, draw starting hand
        if (PlayerTurnCardGameState.PlayerTurnCount == 1)
        {
            _dateDeck.DrawStartingHand();
        }
    }

    public override void Tick()
    {
        if (!_dateThinking)
        {
            //Check for win/loss before turn begins
            Debug.Log("Checking Win/Loss State");
            if (CheckWin())
            {
                StateMachine.ChangeState<WinCardGameState>();
                return;
            }
            else if (CheckLose())
            {
                StateMachine.ChangeState<LoseCardGameState>();
                return;
            }
            else
            {
                _dateDeck._canDraw = true;
                _dateThinking = true;
                _coroutine = StartCoroutine(DateThinkingRoutine(_pauseDuration));
            }
        }
    }

    public override void Exit()
    {
        _dateThinking = false;
        Debug.Log("Date Turn: Exiting...");
    }

    IEnumerator DateThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Date thinking...");
        yield return new WaitForSeconds(pauseDuration);
        _dateDeck.DateTurn();
        yield return new WaitForSeconds(pauseDuration);
        EndOfDateTurn();
    }

    private void DateRandomAction()
    {
        int randomInt = RandomHelper.RandomIntLessThan(6);
        switch (randomInt)
        {
            case 0:
                _date.IncreaseJoy(5);
                Debug.Log("Date Joy +5");
                break;

            case 1:
                _date.IncreaseLove(5);
                Debug.Log("Date Love +5");
                break;

            case 2:
                _date.IncreasePatience(5);
                Debug.Log("Date Patience +5");
                break;

            case 3:
                _date.DecreaseJoy(5);
                Debug.Log("Date Joy -5");
                break;

            case 4:
                _date.DecreaseLove(5);
                Debug.Log("Date Love -5");
                break;

            case 5:
                _date.DecreasePatience(5);
                Debug.Log("Date Patience -5");
                break;
        }
    }

    private void EndOfDateTurn()
    {
        DateTurnEnded?.Invoke();
        Debug.Log("Checking Win/Loss State");
        if (CheckWin())
        {
            StateMachine.ChangeState<WinCardGameState>();
            return;
        }
        if (CheckLose())
        {
            StateMachine.ChangeState<LoseCardGameState>();
            return;
        }
        if (CheckForRoundEnd())
        {
            StateMachine.ChangeState<RoundEndCardGameState>();
            return;
        }
        StateMachine.ChangeState<PlayerTurnCardGameState>();
    }

    private bool CheckForRoundEnd()
    {
        Debug.Log("Checking turn count...");
        if (PlayerTurnCardGameState.PlayerTurnCount >= _playerTurn.TurnsPerRound)
        {
            Debug.LogWarning("Round over: Turn Limit Passed");
            PlayerTurnCardGameState.ResetTurnCount();
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckWin()
    {
        if (_date.Love >= _date.StatCap)
        {
            StopCoroutine(_coroutine);
            return true;
        }
        return false;
    }

    private bool CheckLose()
    {
        //If any stat is below zero, game over
        if (_date.Love <= 0 || _date.Joy <= 0 || _date.Patience <= 0)
        {
            StopCoroutine(_coroutine);
            //StateMachine.ChangeState<LoseCardGameState>();
            return true;
        }
        return false;
    }
}
