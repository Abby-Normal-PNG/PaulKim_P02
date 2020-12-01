using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecords : MonoBehaviour
{
    public int _wins = 0;
    public int _losses = 0;
    public int _datesStarted = 0;
    public int _datesCompleted = 0;

    private void Start()
    {
        LoadData();
    }

    public void Win()
    {
        _wins++;
        EndDate();
    }

    public void Lose()
    {
        _losses++;
        EndDate();
    }

    public void StartDate()
    {
        _datesStarted++;
        SaveData();
    }

    public void EndDate()
    {
        _datesCompleted++;
        SaveData();
    }

    private void SaveData()
    {
        SaveSystem.SaveRecords(this);
    }

    private void LoadData()
    {
        PlayerData data = SaveSystem.LoadRecords();
        if(data != null)
        {
            _wins = data._wins;
            _losses = data._losses;
            _datesStarted = data._datesStarted;
            _datesCompleted = data._datesCompleted;
        }
    }

    public void ClearData()
    {
        _wins = 0;
        _losses = 0;
        _datesStarted = 0;
        _datesCompleted = 0;
        SaveData();
    }
}
