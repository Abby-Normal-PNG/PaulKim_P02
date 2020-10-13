using UnityEngine;

public class AbilityCard : Card
{
    public AbilityCard(string name)
    {
        Name = name;
    }

    public override void Play()
    {
        Debug.Log("Playing Ability Card: " + Name);
    }
}
