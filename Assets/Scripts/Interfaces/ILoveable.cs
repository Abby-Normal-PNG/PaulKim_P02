using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoveable
{
    void IncreaseLove(int amount);
    void IncreaseJoy(int amount);
    void IncreasePatience(int amount);
    void DecreaseLove(int amount);
    void DecreaseJoy(int amount);
    void DecreasePatience(int amount);
}
