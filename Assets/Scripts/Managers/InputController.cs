using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PlayerTurnEnd = delegate { };
    public event Action PressedPlayCard = delegate { };
    public event Action PressedDraw = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };



    public void EndPlayerTurn()
    {
        PlayerTurnEnd?.Invoke();
    }

    public void PlayerPlayCard()
    {
        PressedPlayCard?.Invoke();
    }

    public void PlayerDrawCard()
    {
        PressedDraw?.Invoke();
    }

    public void LeftButtonPressed()
    {
        PressedLeft?.Invoke();
    }

    public void RightButtonPressed()
    {
        PressedRight?.Invoke();
    }
}
