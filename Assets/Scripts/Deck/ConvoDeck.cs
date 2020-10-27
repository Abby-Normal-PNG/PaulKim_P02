using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConvoDeck", menuName = "CardDeck/ConvoDeck")]
public class ConvoDeck : ScriptableObject
{
    [SerializeField] List<ConvoCardData> _convoDeckConfig = new List<ConvoCardData>();
    public List<ConvoCardData> ConvoDeckConfig => _convoDeckConfig;
}
