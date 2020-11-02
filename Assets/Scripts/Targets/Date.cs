using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Gender { Man, Woman, Nonbinary, None }
public class Date : MonoBehaviour, ITargetable, ILoveable
{
    [Header("Flavor Stats")]
    [SerializeField] string _name = "...";
    public string Name => _name;
    [SerializeField] Gender _gender = Gender.None;
    public Gender DateGender => _gender;
    [Header("Mechanical Stats")]
    [SerializeField] int _joy = 50;
    public int Joy => _joy;
    [SerializeField] int _love = 50;
    public int Love => _love;
    [SerializeField] int _patience = 50;
    public int Patience => _patience;
    [SerializeField] int _statCap = 100;
    public int StatCap => _statCap;
    [Header("References")]
    [SerializeField] CardGameSM _stateMachine;

    public static event Action JoyChanged = delegate { };
    public static event Action LoveChanged = delegate { };
    public static event Action PatienceChanged = delegate { };

    private void Start()
    {
        //Debug.Log("Date Name: " + _name);
        //Debug.Log("Date Gender: " + _gender);
    }
    public void Target()
    {
        //Debug.Log(_name + " has been targeted.");
    }
    public void DecreaseJoy(int amount)
    {
        _joy -= amount;
        CapStat(_joy);
        Debug.Log("Joy is now " + _joy);
        JoyChanged?.Invoke();
    }

    public void DecreaseLove(int amount)
    {
        _love -= amount;
        CapStat(_love);
        Debug.Log("Love is now " + _love);
        LoveChanged?.Invoke();
    }

    public void DecreasePatience(int amount)
    {
        _patience -= amount;
        CapStat(_patience);
        Debug.Log("Patience is now " + _patience);
        PatienceChanged?.Invoke();
    }

    public void IncreaseJoy(int amount)
    {
        _joy += amount;
        CapStat(_joy);
        Debug.Log("Joy is now " + _joy);
        JoyChanged?.Invoke();
    }

    public void IncreaseLove(int amount)
    {
        _love += amount;
        CapStat(_love);
        Debug.Log("Love is now " + _love);
        LoveChanged?.Invoke();
    }

    public void IncreasePatience(int amount)
    {
        _patience += amount;
        CapStat(_patience);
        Debug.Log("Patience is now " + _patience);
        PatienceChanged?.Invoke();
    }

    private void CapStat(int value)
    {
        CheckWin();
        CheckLose();
        if(value > _statCap)
        {
            value = _statCap;
        }
        if(value < 0)
        {
            value = 0;
        }
    }

    public bool CheckGender(Gender genderToCheck)
    {
        if(genderToCheck == _gender)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckFirstInitial(string initial)
    {
        if (_name.StartsWith(initial))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckWin()
    {
        if(Love >= StatCap)
        {
            _stateMachine.ChangeState<WinCardGameState>();
        }
    }

    private void CheckLose()
    {
        //If any stat is below zero, game over
        if(Love <= 0 || Joy <= 0 || Patience <= 0)
        {
            _stateMachine.ChangeState<LoseCardGameState>();
        }
    }
}
