using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoveable
{
    //Stat Changes
    void IncreaseLove(int amount);
    void IncreaseJoy(int amount);
    void IncreasePatience(int amount);
    void DecreaseLove(int amount);
    void DecreaseJoy(int amount);
    void DecreasePatience(int amount);

    //Bonus Changes
    void IncreaseLoveBonus(int amount);
    void IncreaseJoyBonus(int amount);
    void IncreasePatienceBonus(int amount);
    void DecreaseLoveBonus(int amount);
    void DecreaseJoyBonus(int amount);
    void DecreasePatienceBonus(int amount);

    //Multiplier Changes
    void ChangeLoveMultiply(float value);
    void ChangeJoyMultiply(float value);
    void ChangePatienceMultiply(float value);
}
