using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConvoCard", menuName = "CardData/ConvoCard")]
public class ConvoCardData : ScriptableObject
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
}
