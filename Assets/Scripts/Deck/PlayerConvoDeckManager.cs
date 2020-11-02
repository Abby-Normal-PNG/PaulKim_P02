using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerConvoDeckManager : MonoBehaviour
{
    [SerializeField] InputController _inputC;
    public InputController InputC => _inputC;

    [SerializeField] ConvoDeck _convoDeck = null;
    [SerializeField] CardView _currentCardView = null;

    Deck<Card> _drawDeck = new Deck<Card>();
    [SerializeField] DrawDeckView _drawDeckView = null;

    Deck<Card> _discardDeck = new Deck<Card>();
    [SerializeField] DiscardDeckView _discardDeckView = null;

    Deck<Card> _playerHand = new Deck<Card>();

    [SerializeField] int _currentCardIndex = 0;

    [SerializeField] int _startHandSize = 3;
    [HideInInspector] public bool _canDraw = false;

    private void Start()
    {
        SetupAbilityDeck();
        DrawStartingHand();
    }

    private void SetupAbilityDeck()
    {
        Debug.Log("Creating ConvoCards...");
        foreach (ConvoCardData convoData in _convoDeck.ConvoDeckConfig)
        {
            ConvoCard newConvoCard = new ConvoCard(convoData);
            _drawDeck.Add(newConvoCard);
        }

        Debug.Log("Shuffling...");
        _drawDeck.Shuffle();
        _drawDeckView.Display(_drawDeck);
        _discardDeckView.DisplayNull();
        _currentCardView.DisplayNull();
    }

    private void DrawStartingHand()
    {
        for(int i = 0; i < _startHandSize; i++)
        {
            DrawCard();
        }
    }

    private void OnEnable()
    {
        _inputC.PressedLeft += OnPressedLeft;
        _inputC.PressedRight += OnPressedRight;
        _inputC.PressedDraw += OnPressedDraw;
        _inputC.PressedPlayCard += OnPressedPlay;
    }

    private void OnDisable()
    {
        _inputC.PressedLeft -= OnPressedLeft;
        _inputC.PressedRight -= OnPressedRight;
        _inputC.PressedDraw -= OnPressedDraw;
        _inputC.PressedPlayCard -= OnPressedPlay;
    }

    private void OnPressedPlay()
    {
        PlayCurrentCard();
    }

    private void OnPressedDraw()
    {
        if (_canDraw)
        {
            DrawCard();
            _canDraw = false;
        }
        else
        {
            Debug.LogWarning("Already drew this turn!");
        }
    }

    private void OnPressedRight()
    {
        SelectNextCard();
    }

    private void OnPressedLeft()
    {
        SelectPrevCard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PrintPlayerHand();
        }
        if(_playerHand.Count == 0)
        {
            //TODO Shift card display logic to Unity Events
            _currentCardView.DisplayNull();
        }
    }

    public void DrawCard()
    {
        Card newCard = _drawDeck.Draw(DeckPosition.Top);
        if (newCard != null)
        {
            Debug.Log("Drew card: " + newCard.Name);
            _playerHand.Add(newCard, DeckPosition.Top);
            _currentCardIndex = _playerHand.Count - 1;
            _currentCardView.Display(_playerHand.Cards[_currentCardIndex]);
            _drawDeckView.Display(_drawDeck);
        }
        else
        {
            Debug.Log("Shuffling discard into deck...");
            _drawDeck.Add(_discardDeck.Cards);
            _drawDeck.Shuffle();
            _discardDeck.Clear();
            _drawDeckView.Display(_drawDeck);
            _discardDeckView.DisplayNull();
        }
    }

    private void PrintPlayerHand()
    {
        for (int i = 0; i < _playerHand.Count; i++)
        {
            Debug.Log("Player Hand Card: " + _playerHand.GetCard(i).Name);
        }
    }

    public void PlayCurrentCard()
    {
        if (_playerHand.Count > 0)
        {
            Card targetCard = _playerHand.Cards[_currentCardIndex];
            targetCard.Play();

            _playerHand.Remove(_currentCardIndex);
            _discardDeck.Add(targetCard);
            _discardDeckView.Display(targetCard);
            Debug.Log("Card added to discard: " + targetCard.Name);

            
            if(_playerHand.Count > 0)
            {
                _currentCardView.Display(_playerHand.TopItem);
                _currentCardIndex = _playerHand.Count - 1;
            }
            else
            {
                _currentCardView.DisplayNull();
                _currentCardIndex = 0;
            }
        }
        else
        {
            Debug.LogWarning("Player Hand: Nothing to play - hand is empty!");
        }
    }

   /* private void PlayTopCard()
    {
        if (_playerHand.Count > 0)
        {
            Card targetCard = _playerHand.TopItem;
            targetCard.Play();
            //TODO Consider expanding Remove to accept a deck position
            _playerHand.Remove(_playerHand.LastIndex);
            _discardDeck.Add(targetCard);
            _discardDeckView.Display(targetCard);
            Debug.Log("Card added to discard: " + targetCard.Name);
        }
        else
        {
            Debug.LogWarning("Player Hand: Nothing to play - hand is empty!");
        }
    }*/

    public void SelectNextCard()
    {
        if(_playerHand.Count > 0)
        {
            if (_currentCardIndex + 1 <= _playerHand.Count - 1)
            {
                //If there's a next card to go to
                _currentCardIndex += 1;
                _currentCardView.Display(_playerHand.Cards[_currentCardIndex]);
            }
            else
            {
                //else return to the beginning
                _currentCardIndex = 0;
                _currentCardView.Display(_playerHand.BottomItem);
            }
        }
        else
        {
            Debug.LogWarning("No cards to select between");
        }
    }

    public void SelectPrevCard()
    {
        if (_playerHand.Count > 0)
        {
            if (_currentCardIndex - 1 >= 0)
            {
                //If there's a previous card to go to
                _currentCardIndex += 1;
                _currentCardView.Display(_playerHand.Cards[_currentCardIndex]);
            }
            else
            {
                //else return to the end
                _currentCardIndex = 0;
                _currentCardView.Display(_playerHand.TopItem);
            }
        }
        else
        {
            Debug.LogWarning("No cards to select between");
        }
    }
}
