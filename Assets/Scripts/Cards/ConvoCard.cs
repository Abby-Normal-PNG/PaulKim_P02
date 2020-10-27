using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoCard : Card
{
    public new Sprite Graphic;
    public CardPlayEffect PlayEffect;
    public new string Description;
    public new Color Color;

    public ConvoCard(ConvoCardData Data)
    {
        Name = Data.Name;
        Graphic = Data.Graphic;
        PlayEffect = Data.PlayEffect;
        Description = Data.Description;
        Color = Data.Color;
    }

    public override void Play()
    {
        ITargetable target = TargetController.CurrentTarget;
        Debug.Log("Playing " + Name + " on target.");
        PlayEffect.Activate(target);
    }
}
