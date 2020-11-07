using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoardCardCommand : ICommand
{
    BoardCardSpawner _boardCardSpawner;
    Card _card;

    BoardCard _spawnedBoardCard;

    public SpawnBoardCardCommand(BoardCardSpawner boardSpawner, Card card)
    {
        _boardCardSpawner = boardSpawner;
        _card = card;
    }

    public void Execute()
    {
        _spawnedBoardCard = _boardCardSpawner.SpawnBoardCard(_card);
    }

    public BoardCard Execute(Card card)
    {
        _spawnedBoardCard = _boardCardSpawner.SpawnBoardCard(card);
        return _spawnedBoardCard;
    }

    public void Undo()
    {
        _boardCardSpawner.RemoveToken(_spawnedBoardCard);
    }
}
