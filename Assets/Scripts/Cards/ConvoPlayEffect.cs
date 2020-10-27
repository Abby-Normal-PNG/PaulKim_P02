using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatEffectType { IncJoy, IncLove, IncPatience, DecJoy, DecLove, DecPatience}
[CreateAssetMenu(fileName = "NewConvoPlayEffect", menuName = "PlayEffects/Convo")]
public class ConvoPlayEffect : CardPlayEffect
{
    [SerializeField] StatEffectType _type = StatEffectType.IncJoy;
    [SerializeField] int _amount = 1;

    public override void Activate(ITargetable target)
    {
        //test for damageable
        ILoveable objectToTarget = target as ILoveable;

        if (objectToTarget != null)
        {
            switch (_type)
            {
                case StatEffectType.IncJoy:
                    Debug.Log("Increasing target's Joy.");
                    objectToTarget.IncreaseJoy(_amount);
                    break;

                case StatEffectType.IncLove:
                    Debug.Log("Increasing target's Love.");
                    objectToTarget.IncreaseLove(_amount);
                    break;

                case StatEffectType.IncPatience:
                    Debug.Log("Increasing target's Patience.");
                    objectToTarget.IncreasePatience(_amount);
                    break;

                case StatEffectType.DecJoy:
                    Debug.Log("Decreasing target's Joy.");
                    objectToTarget.DecreaseJoy(_amount);
                    break;

                case StatEffectType.DecLove:
                    Debug.Log("Decreasing target's Love.");
                    objectToTarget.DecreaseLove(_amount);
                    break;

                case StatEffectType.DecPatience:
                    Debug.Log("Decreasing target's Patience.");
                    objectToTarget.DecreasePatience(_amount);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Can not " + _type + " of target!");
        }
    }
}
