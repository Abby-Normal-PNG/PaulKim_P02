using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCardSpawner : MonoBehaviour
{
    [SerializeField] BoardCard _boardCardPrefab = null;
    [SerializeField] Transform _spawnPosition = null;
    public Transform SpawnPosition => _spawnPosition;
    [SerializeField] CardType _cardType = CardType.Generic;

    public BoardCard _spawnedBoardCard;

    public BoardCard SpawnBoardCard(Card card)
    {
        if (CardTypesMatch(card.CardType))
        {
            _spawnedBoardCard = Instantiate(_boardCardPrefab, _spawnPosition.transform.position, _spawnPosition.transform.rotation);
            _spawnedBoardCard.SetTexture(card);
            return _spawnedBoardCard;
        }
        else
        {
            return null;
        }
    }

    private bool CardTypesMatch(CardType typeToMatch)
    {
        if(_cardType == CardType.Generic)
        {
            return true;
        }
        if(typeToMatch == _cardType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveToken(BoardCard boardCardToRemove)
    {
        Destroy(boardCardToRemove.gameObject);
    }

    public void RemoveBoardCard()
    {
        if(_spawnedBoardCard != null)
        {
            Destroy(_spawnedBoardCard.gameObject);
        }
    }
}
