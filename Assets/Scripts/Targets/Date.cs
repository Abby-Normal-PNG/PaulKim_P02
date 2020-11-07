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
    [Header("Joy")]
    [SerializeField] int _startJoy = 50;
    [SerializeField] float _joyMultiplier = 1f;
    [SerializeField] int _joyBonus = 0;
    private int _joy = 50;
    public int Joy => _joy;

    [Header("Love")]
    [SerializeField] int _startLove = 50;
    [SerializeField] float _loveMultiplier = 1f;
    [SerializeField] int _loveBonus = 0;
    private int _love = 50;
    public int Love => _love;

    [Header("Joy")]
    [SerializeField] int _startPatience = 50;
    [SerializeField] float _patienceMultiplier = 1f;
    [SerializeField] int _patienceBonus = 0;
    private int _patience = 50;
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

    public void ResetStats()
    {
        _joy = _startJoy;
        _love = _startLove;
        _patience = _startPatience;
        ResetMods();
    }

    public void ResetMods()
    {
        _joyBonus = 0;
        _joyMultiplier = 1f;
        _loveBonus = 0;
        _loveMultiplier = 1f;
        _patienceBonus = 0;
        _patienceMultiplier = 1f;
    }

    public void DecreaseJoy(int amount)
    {
        _joy = _joy - (int)(amount * _joyMultiplier) + _joyBonus;
        CapStat(_joy);
        Debug.Log("Joy is now " + _joy);
        JoyChanged?.Invoke();
    }

    public void DecreaseLove(int amount)
    {
        _love = _love - (int)(amount * _loveMultiplier) + _loveBonus;
        CapStat(_love);
        Debug.Log("Love is now " + _love);
        LoveChanged?.Invoke();
    }

    public void DecreasePatience(int amount)
    {
        _patience = _patience - (int)(amount * _patienceMultiplier) + _patienceBonus;
        CapStat(_patience);
        Debug.Log("Patience is now " + _patience);
        PatienceChanged?.Invoke();
    }

    public void IncreaseJoy(int amount)
    {
        _joy = _joy + (int)(amount * _joyMultiplier) + _joyBonus;
        CapStat(_joy);
        Debug.Log("Joy is now " + _joy);
        JoyChanged?.Invoke();
    }

    public void IncreaseLove(int amount)
    {
        _love = _love + (int)(amount * _loveMultiplier) + _loveBonus;
        CapStat(_love);
        Debug.Log("Love is now " + _love);
        LoveChanged?.Invoke();
    }

    public void IncreasePatience(int amount)
    {
        _patience = _patience + (int)(amount * _patienceMultiplier) + _patienceBonus;
        CapStat(_patience);
        Debug.Log("Patience is now " + _patience);
        PatienceChanged?.Invoke();
    }

    private void CapStat(int value)
    {
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

    public void IncreaseLoveBonus(int amount)
    {
        _loveBonus += amount;
    }

    public void IncreaseJoyBonus(int amount)
    {
        _joyBonus += amount;
    }

    public void IncreasePatienceBonus(int amount)
    {
        _patienceBonus += amount;
    }

    public void DecreaseLoveBonus(int amount)
    {
        _loveBonus -= amount;
    }

    public void DecreaseJoyBonus(int amount)
    {
        _joyBonus -= amount;
    }

    public void DecreasePatienceBonus(int amount)
    {
        _patienceBonus -= amount;
    }

    public void ChangeLoveMultiply(float value)
    {
        _loveMultiplier = value;
    }

    public void ChangeJoyMultiply(float value)
    {
        _joyMultiplier = value;
    }

    public void ChangePatienceMultiply(float value)
    {
        _patienceMultiplier = value;
    }
}
