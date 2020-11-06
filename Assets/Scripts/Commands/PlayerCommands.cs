using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    [SerializeField] BoardSpawner _boardSpawner;
    [SerializeField] ConvoCardData _cardData;
    private ConvoCard _card;
    private BoardCardSpawner _boardCardSpawner = null;

    Camera _camera = null;
    RaycastHit _hitInfo;

    CommandInvoker _commandInvoker = new CommandInvoker();

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        _card = new ConvoCard(_cardData);
    }

    private void Update()
    {
        //Spawn Command
        if (Input.GetMouseButtonDown(0))
        {
            GetNewMouseHit();
            AssignBoardCardSpawner();
            SpawnBoardCard();
        }
        //Buff Command
        if (Input.GetMouseButtonDown(1))
        {
            GetNewMouseHit();
            BuffToken();
        }
        //Undo Command
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
    }

    void GetNewMouseHit()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out _hitInfo, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + _hitInfo.transform.name);
        }
    }

    void SpawnBoardCard()
    {
        if(_boardCardSpawner != null)
        {
            ICommand spawnBoardCardCommand = new SpawnBoardCardCommand(_boardCardSpawner, _card);
            _commandInvoker.ExecuteCommand(spawnBoardCardCommand);
        }
    }

    void AssignBoardCardSpawner()
    {
        _boardCardSpawner = _hitInfo.collider.gameObject.GetComponent<BoardCardSpawner>();
    }

    void SpawnToken()
    {
        ICommand spawnTokenCommand = new SpawnTokenCommand(_boardSpawner, _hitInfo.point);
        _commandInvoker.ExecuteCommand(spawnTokenCommand);
    }

    void BuffToken()
    {
        IBuffable buffableUnit = _hitInfo.transform.GetComponent<IBuffable>();
        if(buffableUnit != null)
        {
            ICommand buffCommand = new BuffCommand(buffableUnit);
            _commandInvoker.ExecuteCommand(buffCommand);
        }
    }

    void Undo()
    {
        _commandInvoker.UndoCommand();
    }
}
