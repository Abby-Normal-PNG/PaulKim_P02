using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsUIController : MonoBehaviour
{
    [SerializeField] PlayerRecords _records = null;
    [SerializeField] Text _winsText;
    [SerializeField] Text _lossesText;
    [SerializeField] Text _startText;
    [SerializeField] Text _completedText;

    public void UpdateRecordsText()
    {
        _winsText.text = " " + _records._wins;
        _lossesText.text = " " + _records._losses;
        _startText.text = " " + _records._datesStarted;
        _completedText.text = " " + _records._datesCompleted;
    }
}
