using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatMultiType { MultiJoy, MultiLove, MultiPatience }
[CreateAssetMenu(fileName = "NewEnviroPlayEffect", menuName = "PlayEffects/Enviro/Multiply")]
public class EnviroMultiplyPlayEffect : CardPlayEffect
{
    [SerializeField] StatMultiType _type = StatMultiType.MultiJoy;
    [SerializeField] float _amount = 1f;

    public override void Activate(ITargetable target)
    {
        //test for damageable
        ILoveable objectToTarget = target as ILoveable;

        if (objectToTarget != null)
        {
            switch (_type)
            {
                case StatMultiType.MultiJoy:
                    Debug.Log("Changing target's Joy Multiplier.");
                    objectToTarget.ChangeJoyMultiply(_amount);
                    break;

                case StatMultiType.MultiLove:
                    Debug.Log("Changing target's Love Multiplier.");
                    objectToTarget.ChangeLoveMultiply(_amount);
                    break;

                case StatMultiType.MultiPatience:
                    Debug.Log("Changing target's Patience Multiplier.");
                    objectToTarget.ChangePatienceMultiply(_amount);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Can not " + _type + " of target!");
        }
    }
}
