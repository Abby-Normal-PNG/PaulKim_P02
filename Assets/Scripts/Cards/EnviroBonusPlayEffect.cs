using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatBonusType { IncJoyBonus, IncLoveBonus, IncPatienceBonus, 
                            DecJoyBonus, DecLoveBonus, DecPatienceBonus }
[CreateAssetMenu(fileName = "NewEnviroPlayEffect", menuName = "PlayEffects/Enviro/Bonus")]
public class EnviroBonusPlayEffect : CardPlayEffect
{
    [SerializeField] StatBonusType _type = StatBonusType.IncJoyBonus;
    [SerializeField] int _amount = 1;

    public override void Activate(ITargetable target)
    {
        //test for damageable
        ILoveable objectToTarget = target as ILoveable;

        if (objectToTarget != null)
        {
            switch (_type)
            {
                case StatBonusType.IncJoyBonus:
                    Debug.Log("Increasing target's Joy Bonus.");
                    objectToTarget.IncreaseJoyBonus(_amount);
                    break;

                case StatBonusType.IncLoveBonus:
                    Debug.Log("Increasing target's Love Bonus.");
                    objectToTarget.IncreaseLoveBonus(_amount);
                    break;

                case StatBonusType.IncPatienceBonus:
                    Debug.Log("Increasing target's Patience Bonus.");
                    objectToTarget.IncreasePatienceBonus(_amount);
                    break;

                case StatBonusType.DecJoyBonus:
                    Debug.Log("Decreasing target's Joy Bonus.");
                    objectToTarget.DecreaseJoyBonus(_amount);
                    break;

                case StatBonusType.DecLoveBonus:
                    Debug.Log("Decreasing target's Love Bonus.");
                    objectToTarget.DecreaseLoveBonus(_amount);
                    break;

                case StatBonusType.DecPatienceBonus:
                    Debug.Log("Decreasing target's Patience Bonus.");
                    objectToTarget.DecreasePatienceBonus(_amount);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Can not " + _type + " of target!");
        }
    }
}
