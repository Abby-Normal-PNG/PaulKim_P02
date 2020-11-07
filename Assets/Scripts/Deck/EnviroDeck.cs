using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnviroDeck", menuName = "CardDeck/EnviroDeck")]
public class EnviroDeck : ScriptableObject
{
    [SerializeField] List<EnviroCardData> _enviroDeckConfig = new List<EnviroCardData>();
    public List<EnviroCardData> EnviroDeckConfig => _enviroDeckConfig;
}
