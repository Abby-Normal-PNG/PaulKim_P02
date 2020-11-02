using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateTurnCardGameState : CardGameState
{
    public static event Action DateTurnBegan;
    public static event Action DateTurnEnded;

    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Date Turn: Entering...");
        DateTurnBegan?.Invoke();

        StartCoroutine(DateThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Date Turn: Exiting...");
    }

    IEnumerator DateThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Date thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Date performs action");
        DateTurnEnded?.Invoke();
        //Back to player
        CheckForRoundEnd();
    }

    private void CheckForRoundEnd()
    {
        Debug.Log("Checking turn count...");
        if (PlayerTurnCardGameState.PlayerTurnCount >= PlayerTurnCardGameState.TurnsPerRound)
        {
            Debug.LogWarning("Round over: Turn Limit Passed");
            PlayerTurnCardGameState.ResetTurnCount();
            StateMachine.ChangeState<RoundEndCardGameState>();
        }
        else
        {
            StateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }
}
