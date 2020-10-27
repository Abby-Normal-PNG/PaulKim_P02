using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };

    [SerializeField] KeyCode _confirmKey = KeyCode.Space;
    [SerializeField] KeyCode _cancelKey = KeyCode.Escape;
    [SerializeField] KeyCode _leftKey = KeyCode.A;
    [SerializeField] KeyCode _rightKey = KeyCode.D;

    private void Update()
    {
        DetectConfirm();
        DetectCancel();
        DetectLeft();
        DetectRight();
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(_confirmKey))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(_cancelKey))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectLeft()
    {
        if (Input.GetKeyDown(_leftKey))
        {
            PressedLeft?.Invoke();
        }
    }

    private void DetectRight()
    {
        if (Input.GetKeyDown(_rightKey))
        {
            PressedRight?.Invoke();
        }
    }
}
