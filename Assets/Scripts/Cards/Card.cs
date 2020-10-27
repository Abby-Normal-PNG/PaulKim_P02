using UnityEngine;

public abstract class Card
{
    public string Name { get; protected set; } = "...";
    public Sprite Graphic { get; protected set; }
    public string Description { get; protected set; } = "...";
    public Color Color { get; protected set; } = Color.white;
    public abstract void Play();
}