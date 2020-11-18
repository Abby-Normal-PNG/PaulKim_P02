using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameUIController : MonoBehaviour
{
    [SerializeField] Text _dateThinkingTextUI = null;
    [SerializeField] Date _date = null;
    public Date Date => _date;
    [SerializeField] Slider _dateJoySlider = null;
    [SerializeField] Slider _dateLoveSlider = null;
    [SerializeField] Slider _datePatienceSlider = null;

    private void OnEnable()
    {
        DateTurnCardGameState.DateTurnBegan += OnEnemyTurnBegan;
        DateTurnCardGameState.DateTurnEnded += OnEnemyTurnEnded;
        Date.JoyChanged += UpdateJoySlider;
        Date.LoveChanged += UpdateLoveSlider;
        Date.PatienceChanged += UpdatePatienceSlider;
    }

    private void OnDisable()
    {
        DateTurnCardGameState.DateTurnBegan -= OnEnemyTurnBegan;
        DateTurnCardGameState.DateTurnEnded -= OnEnemyTurnEnded;
        Date.JoyChanged -= UpdateJoySlider;
        Date.LoveChanged -= UpdateLoveSlider;
        Date.PatienceChanged -= UpdatePatienceSlider;
    }

    void Start()
    {
        _dateThinkingTextUI.gameObject.SetActive(false);
        PrepareSliders();
    }

    void OnEnemyTurnBegan()
    {
        _dateThinkingTextUI.gameObject.SetActive(true);
        UpdateDateText(_date.Name + " is thinking...");
    }

    void OnEnemyTurnEnded()
    {
        _dateThinkingTextUI.gameObject.SetActive(false);
    }

    void UpdateJoySlider()
    {
        _dateJoySlider.value = _date.Joy;
    }

    void UpdateLoveSlider()
    {
        _dateLoveSlider.value = _date.Love;
    }

    void UpdatePatienceSlider()
    {
        _datePatienceSlider.value = _date.Patience;
    }

    void PrepareSliders()
    {
        //Prep Joy Slider
        _dateJoySlider.minValue = 0;
        _dateJoySlider.maxValue = _date.StatCap;
        _dateJoySlider.value = _date.Joy;
        //Prep Love Slider
        _dateLoveSlider.minValue = 0;
        _dateLoveSlider.maxValue = _date.StatCap;
        _dateLoveSlider.value = _date.Love;
        //Prep PatienceSlider
        _datePatienceSlider.minValue = 0;
        _datePatienceSlider.maxValue = _date.StatCap;
        _datePatienceSlider.value = _date.Patience;
    }

    public void UpdateDateText(string message)
    {
        _dateThinkingTextUI.text = message;
    }
}
