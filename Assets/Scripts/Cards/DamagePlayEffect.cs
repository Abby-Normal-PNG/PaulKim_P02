using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamagePlayEffect", menuName = "CardData/PlayEffects/Damage")]
public class DamagePlayEffect : CardPlayEffect
{
    [SerializeField] int _damageAmount = 1;

    public override void Activate(ITargetable target)
    {
        //test for damageable
        IDamageable objectToDamage = target as IDamageable;

        if(objectToDamage != null)
        {
            Debug.Log("Adding damage to target.");
            objectToDamage.TakeDamage(_damageAmount);
        }
        else
        {
            Debug.Log("Target is not damageable.");
        }
    }
}
