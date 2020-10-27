using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender { Man, Woman, Nonbinary, None }
public class Date : MonoBehaviour, ITargetable, ILoveable
{
    [Header("Flavor Stats")]
    [SerializeField] string _name = "...";
    [SerializeField] Gender _gender = Gender.None;
    [Header("Mechanical Stats")]
    [SerializeField] int _joy = 50;
    [SerializeField] int _love = 50;
    [SerializeField] int _patience = 50;

    public void Target()
    {
        Debug.Log("Date has been targeted.");
    }
    public void DecreaseJoy(int amount)
    {
        _joy -= amount;
        CapStat(_joy);
        Debug.Log("Joy is now " + _joy);
    }

    public void DecreaseLove(int amount)
    {
        _love -= amount;
        CapStat(_love);
        Debug.Log("Love is now " + _love);
    }

    public void DecreasePatience(int amount)
    {
        _patience -= amount;
        CapStat(_patience);
        Debug.Log("Patience is now " + _patience);
    }

    public void IncreaseJoy(int amount)
    {
        _joy += amount;
        CapStat(_joy);
        Debug.Log("Joy is now " + _joy);
    }

    public void IncreaseLove(int amount)
    {
        _love += amount;
        CapStat(_love);
        Debug.Log("Love is now " + _love);
    }

    public void IncreasePatience(int amount)
    {
        _patience += amount;
        CapStat(_patience);
        Debug.Log("Patience is now " + _patience);
    }

    private void CapStat(int stat)
    {
        if(stat > 100)
        {
            stat = 100;
        }
        if(stat < 0)
        {
            stat = 0;
        }
    }
}
