using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvoCard : Card
{
    public CardPlayEffect PlayEffect;

    public ConvoCard(ConvoCardData Data)
    {
        Name = Data.Name;
        Graphic = Data.Graphic;
        PlayEffect = Data.PlayEffect;
        Description = Data.Description;
        Color = Data.Color;
        CardType = Data.CardType;
        CardTexture = Data.CardTexture;
    }

    public override void Play()
    {
        ITargetable target = TargetController.CurrentTarget;
        Debug.Log("Playing " + Name + " on target.");
        PlayEffect.Activate(target);
    }
}
