using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int _wins;
    public int _losses;
    public int _datesStarted;
    public int _datesCompleted;

    public PlayerData(PlayerRecords records)
    {
        _wins = records._wins;
        _losses = records._losses;
        _datesStarted = records._datesStarted;
        _datesCompleted = records._datesCompleted;
    }
}
