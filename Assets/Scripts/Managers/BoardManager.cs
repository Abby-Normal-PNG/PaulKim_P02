using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] BoardCardSpawner[] _cardSpawners;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ClearBoard();
        }
    }

    public void ClearBoard()
    {
        if(_cardSpawners.Length > 0)
        {
            Debug.Log("Clearing Board...");
            foreach(BoardCardSpawner spawner in _cardSpawners)
            {
                spawner.RemoveBoardCard();
            }
        }
    }
}
