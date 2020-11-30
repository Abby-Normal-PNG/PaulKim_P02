using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateDeckManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ConvoDeck _convoDeck = null;
    [SerializeField] EnviroDeck _enviroDeck = null;


    [SerializeField] List<BoardCardSpawner> _dateField = new List<BoardCardSpawner>();


    [SerializeField] CardGameUIController _uiController = null;

    [Header("Values")]
    [SerializeField] int _currentCardIndex = 0;
    [SerializeField] int _shuffles = 3;
    [SerializeField] int _startHandSize = 3;
    [SerializeField] int _handLimit = 7;
    [HideInInspector] public bool _canDraw = false;

    Deck<Card> _drawDeck = new Deck<Card>();

    Deck<Card> _discardDeck = new Deck<Card>();

    Deck<Card> _dateHand = new Deck<Card>();

    Deck<Card> _dateHandPlayable = new Deck<Card>();

    private Card _cardToPlace = null;
    public Card CardToPlace => _cardToPlace;

    private BoardCardSpawner _spaceToPlaceAt = null;
    public BoardCardSpawner SpaceToPlaceAt => _spaceToPlaceAt;

    private List<BoardCardSpawner> _dateFieldOpen = new List<BoardCardSpawner>();

    private string _turnMessage = "...";

    CommandInvoker _commandInvoker = new CommandInvoker();

    private void Start()
    {
        //SetupDrawDeck();
    }

    public void SetupDrawDeck()
    {
        Debug.Log("Date: Creating ConvoCards...");
        foreach (ConvoCardData convoData in _convoDeck.ConvoDeckConfig)
        {
            ConvoCard newConvoCard = new ConvoCard(convoData);
            _drawDeck.Add(newConvoCard);
        }
        Debug.Log("Date: Creating EnviroCards...");
        foreach (EnviroCardData enviroData in _enviroDeck.EnviroDeckConfig)
        {
            EnviroCard newEnviroCard = new EnviroCard(enviroData);
            _drawDeck.Add(newEnviroCard);
        }

        Debug.Log("Date: Shuffling...");
        for (int i = 0; i < _shuffles; i++)
        {
            _drawDeck.Shuffle();
        }
    }

    public void DrawStartingHand()
    {
        if (_drawDeck.Count < _startHandSize)
        {
            Debug.Log("Date: Shuffling discard into deck...");
            _drawDeck.Add(_discardDeck.Cards);
            _drawDeck.Shuffle();
            _discardDeck.Clear();
        }
        for (int i = 0; i < _startHandSize; i++)
        {
            DrawCard();
        }
    }

    private void OnPressedPlay()
    {
        PlayCurrentCard();
    }

    private void PrepareDraw()
    {
        if (_dateHand.Count < _handLimit)
        {
            if (_canDraw)
            {
                DrawCard();
            }
            else
            {
                Debug.LogWarning("Date: Already drew this turn!");
            }
        }
        else
        {
            Debug.LogWarning("Date Hand: Hand size too large, discarding cards...");
            DiscardExcessRandom();
            DrawCard();
        }
    }
    private void DiscardExcessRandom()
    {
        while (_dateHand.Count >= _handLimit)
        {
            _currentCardIndex = UnityEngine.Random.Range(0, _dateHand.Count);
            DiscardCurrentCard();
        }
    }
    private void DiscardExcess()
    {
        while (_dateHand.Count >= _handLimit)
        {
            _currentCardIndex = 0;
            DiscardCurrentCard();
        }
    }

    public void DrawCard()
    {
        Card newCard = _drawDeck.Draw(DeckPosition.Top);
        if (newCard != null)
        {
            Debug.Log("Drew card: " + newCard.Name);
            _dateHand.Add(newCard, DeckPosition.Top);
            _currentCardIndex = _dateHand.Count - 1;
            _canDraw = false;
        }
        else
        {
            Debug.Log("Shuffling discard into deck...");
            _drawDeck.Add(_discardDeck.Cards);
            _drawDeck.Shuffle();
            _discardDeck.Clear();
            _canDraw = true;
        }
    }

    public void PlayCurrentCard()
    {
        if (_dateHand.Count > 0)
        {
            Card targetCard = _dateHand.Cards[_currentCardIndex];
            _cardToPlace = targetCard;
        }
        else
        {
            Debug.LogWarning("Date: Nothing to play - hand is empty!");
        }
    }

    public void SelectNextCard()
    {
        if (_dateHand.Count > 0)
        {
            if (_currentCardIndex + 1 <= _dateHand.Count - 1)
            {
                //If there's a next card to go to
                _currentCardIndex += 1;
            }
            else
            {
                //else return to the beginning
                _currentCardIndex = 0;
            }
        }
        else
        {
            Debug.LogWarning("Date: No cards to select between");
        }
    }

    public void SelectPrevCard()
    {
        if (_dateHand.Count > 0)
        {
            if (_currentCardIndex - 1 >= 0)
            {
                //If there's a previous card to go to
                _currentCardIndex -= 1;
            }
            else
            {
                //else return to the end
                _currentCardIndex = _dateHand.Count - 1;
            }
        }
        else
        {
            Debug.LogWarning("Date: No cards to select between");
        }
    }

    public void DiscardCurrentCard()
    {
        Card targetCard = _dateHand.Cards[_currentCardIndex];
        _dateHand.Remove(_currentCardIndex);
        _discardDeck.Add(targetCard);
        Debug.Log("Card added to discard: " + targetCard.Name);
        Debug.Log("Player Hand Count: " + _dateHand.Count);
        if (_dateHand.Count > 0)
        {
            _currentCardIndex = _dateHand.Count - 1;
        }
        else
        {
            _currentCardIndex = 0;
        }
    }

    public void DateTurn()
    {
        PrepareDraw();
        CheckPlayableSpaces();
        CheckPlayableCards();
        if(_dateHandPlayable.Count == 0)
        {
            PassTurn();
        }
        else
        {
            PlayRandomCard();
        }
    }

    private void CheckPlayableSpaces()
    {
        _dateFieldOpen = new List<BoardCardSpawner>();
        foreach(BoardCardSpawner space in _dateField)
        {
            if(space.SpaceFilled == false)
            {
                _dateFieldOpen.Add(space);
            }
        }
    }

    private void CheckPlayableCards()
    {
        _dateHandPlayable.Clear();
        //Run this for each card in the hand
        for(int i = 0; i < _dateHand.Count; i++)
        {
            bool canPlay = false;
            //Check if any of the field spaces are open for the right
            //type of card
            foreach (BoardCardSpawner space in _dateFieldOpen)
            {
                if (space.CardTypesMatch(_dateHand.Cards[i].CardType))
                {
                    canPlay = true;
                }
            }
            if (canPlay)
            {
                _dateHandPlayable.Add(_dateHand.Cards[i]);
                _dateHand.Remove(i);
            }
        }
    }

    private void PassTurn()
    {
        _uiController.UpdateDateText(_uiController.Date.Name + " passes this turn.");
    }

    private void PlayRandomCard()
    {
        int cardIndex = RandomHelper.RandomIntLessThan(_dateHandPlayable.Count);
        Card targetCard = _dateHandPlayable.Cards[cardIndex];
        _cardToPlace = targetCard;
        bool spaceFound = false;
        //Taking out the card to place and returning the rest to the hand
        _dateHandPlayable.Remove(cardIndex);
        _dateHand.Add(_dateHandPlayable.Cards);
        foreach (BoardCardSpawner space in _dateFieldOpen)
        {
            if(spaceFound == false)
            {
                if (space.CardTypesMatch(_cardToPlace.CardType))
                {
                    _spaceToPlaceAt = space;
                    spaceFound = true;
                }
            }
        }
        SpawnBoardCard(_spaceToPlaceAt, _cardToPlace);
    }

    void SpawnBoardCard(BoardCardSpawner cardSpawner, Card card)
    {
        if (cardSpawner != null)
        {
            SpawnBoardCardCommand spawnBoardCardCommand = new SpawnBoardCardCommand(cardSpawner, card);
            BoardCard spawnedBoardCard;
            spawnedBoardCard = _commandInvoker.ExecuteReturnBoardCard(spawnBoardCardCommand, card);
            //Checking to see if board card is properly spawned before continuing turn
            if (spawnedBoardCard != null)
            {
                card.Play();
                _uiController.UpdateDateText(_uiController.Date.Name + " plays " + card.Name);
            }
        }
    }

    public void ClearDecks()
    {
        _drawDeck.Clear();
        _discardDeck.Clear();
        _dateHand.Clear();
    }
}
