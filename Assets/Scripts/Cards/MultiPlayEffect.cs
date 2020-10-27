using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMultiPlayEffect", menuName = "CardData/PlayEffects/Multi")]
public class MultiPlayEffect : CardPlayEffect
{
    [SerializeField] List<CardPlayEffect> _effects = new List<CardPlayEffect>();
    public override void Activate(ITargetable target)
    {
        foreach(CardPlayEffect effect in _effects)
        {
            effect.Activate(target);
        }
    }
}
