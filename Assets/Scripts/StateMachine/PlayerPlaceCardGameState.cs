using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceCardGameState : CardGameState
{
    [SerializeField] PlayerConvoDeckManager _playerManager;
    [SerializeField] Canvas _placeCardCanvas;
    private Card _card;
    private BoardCardSpawner _boardCardSpawner = null;

    Camera _camera = null;
    RaycastHit _hitInfo;

    CommandInvoker _commandInvoker = new CommandInvoker();

    public override void Enter()
    {
        _camera = Camera.main;
        _card = _playerManager.CardToPlace;
        _placeCardCanvas.gameObject.SetActive(true);
    }

    public override void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetNewMouseHit();
            AssignBoardCardSpawner();
            SpawnBoardCard();
        }
    }

    public override void Exit()
    {
        _placeCardCanvas.gameObject.SetActive(false);
    }

    void GetNewMouseHit()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + _hitInfo.transform.name);
        }
    }

    void AssignBoardCardSpawner()
    {
        _boardCardSpawner = _hitInfo.collider.gameObject.GetComponent<BoardCardSpawner>();
    }

    void SpawnBoardCard()
    {
        if (_boardCardSpawner != null)
        {
            SpawnBoardCardCommand spawnBoardCardCommand = new SpawnBoardCardCommand(_boardCardSpawner, _card);
            BoardCard spawnedBoardCard;
            spawnedBoardCard = _commandInvoker.ExecuteReturnBoardCard(spawnBoardCardCommand, _card);
            //Checking to see if board card is properly spawned before continuing turn
            if(spawnedBoardCard != null)
            {
                _card.Play();
                _playerManager.DiscardCurrentCard();
                StateMachine.ChangeState<DateTurnCardGameState>();
            }
        }
    }

    public void AbandonCard()
    {
        _playerManager.DiscardCurrentCard();
        StateMachine.ChangeState<DateTurnCardGameState>();
    }

    public void ClearBoard()
    {
        _commandInvoker.UndoAll();
    }
}
