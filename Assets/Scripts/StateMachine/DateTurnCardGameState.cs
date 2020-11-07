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

    private Coroutine _coroutine;

    public override void Enter()
    {
        Debug.Log("Date Turn: Entering...");
        DateTurnBegan?.Invoke();

        _coroutine = StartCoroutine(DateThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Date Turn: Exiting...");
    }

    IEnumerator DateThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Checking Win/Loss State");
        Debug.Log("Date thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Date performs action");
        EndOfDateTurn();
    }

    private void EndOfDateTurn()
    {
        DateTurnEnded?.Invoke();
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
        if (PlayerTurnCardGameState.PlayerTurnCount >= PlayerTurnCardGameState.TurnsPerRound)
        {
            Debug.LogWarning("Round over: Turn Limit Passed");
            PlayerTurnCardGameState.ResetTurnCount();
            //StateMachine.ChangeState<RoundEndCardGameState>();
            return true;
        }
        else
        {
            //StateMachine.ChangeState<PlayerTurnCardGameState>();
            return false;
        }
    }

    private bool CheckWin()
    {
        if (_date.Love >= _date.StatCap)
        {
            StopCoroutine(_coroutine);
            //StateMachine.ChangeState<WinCardGameState>();
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
