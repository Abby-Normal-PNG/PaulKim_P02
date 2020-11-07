using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConvoCard", menuName = "CardData/EnviroCard")]
public class EnviroCardData : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] Color _color = Color.white;
    public Color Color => _color;

    [SerializeField] CardPlayEffect _playEffect = null;
    public CardPlayEffect PlayEffect => _playEffect;

    [SerializeField] [TextArea] string _description = "...";
    public string Description => _description;

    private CardType _cardType = CardType.Environment;
    public CardType CardType => _cardType;

    [SerializeField] Texture2D _cardTexture = null;
    public Texture2D CardTexture => _cardTexture;
}
