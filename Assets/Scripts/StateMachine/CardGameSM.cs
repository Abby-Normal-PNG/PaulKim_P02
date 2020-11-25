using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameSM : StateMachine
{
    [SerializeField] InputController _input;
    public InputController Input => _input;
    [SerializeField] BGMPlayer _bgm;
    public BGMPlayer BGM => _bgm;
    private void Start()
    {
        ChangeState<MenuCardGameState>();
    }
}
