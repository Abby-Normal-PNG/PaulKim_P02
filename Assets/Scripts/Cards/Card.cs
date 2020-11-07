using UnityEngine;

public enum CardType { Generic, Ability, Conversation, Environment, Date }
public abstract class Card
{
    public string Name { get; protected set; } = "...";
    public Sprite Graphic { get; protected set; }
    public string Description { get; protected set; } = "...";
    public Color Color { get; protected set; } = Color.white;
    public CardType CardType { get; protected set; } = CardType.Generic;
    public Texture2D CardTexture { get; protected set; }
    public abstract void Play();
}