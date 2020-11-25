using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceCardGameState : CardGameState
{
    [SerializeField] PlayerDeckManager _playerManager;
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
            StartCoroutine(PlaceCardOnBoard(0.5f));
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

    private IEnumerator PlaceCardOnBoard(float duration)
    {
        SpawnBoardCardCommand spawnBoardCardCommand = new SpawnBoardCardCommand(_boardCardSpawner, _card);
        BoardCard spawnedBoardCard;
        if (spawnBoardCardCommand.CanExecute())
        {
            Vector2 newPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 oldPos = new Vector2(_playerManager.CardToPlaceGO.transform.position.x, 
                _playerManager.CardToPlaceGO.transform.position.x);
            LeanTween.move(_playerManager.CardToPlaceGO, newPos, duration);
            LeanTween.scaleX(_playerManager.CardToPlaceGO, 0.75f, duration);
            LeanTween.scaleY(_playerManager.CardToPlaceGO, 0.75f, duration);
            yield return new WaitForSeconds(duration);
            spawnedBoardCard = _commandInvoker.ExecuteReturnBoardCard(spawnBoardCardCommand, _card);
            if (spawnedBoardCard != null)
            {
                _card.Play();
                _playerManager.DiscardPlayedCard();
                Destroy(_playerManager.CardToPlaceGO);
                StateMachine.ChangeState<DateTurnCardGameState>();
            }
            else
            {
                LeanTween.move(_playerManager.CardToPlaceGO, oldPos, duration);
                LeanTween.scaleX(_playerManager.CardToPlaceGO, 1f, duration);
                LeanTween.scaleY(_playerManager.CardToPlaceGO, 1f, duration);
            }
        }
        
    }
}
