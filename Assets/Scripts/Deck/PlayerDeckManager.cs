using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputController _inputC;
    public InputController InputC => _inputC;

    [SerializeField] ConvoDeck _convoDeck = null;
    [SerializeField] EnviroDeck _enviroDeck = null;
    [SerializeField] CardView _currentCardView = null;

    Deck<Card> _drawDeck = new Deck<Card>();
    [SerializeField] DrawDeckView _drawDeckView = null;

    Deck<Card> _discardDeck = new Deck<Card>();
    [SerializeField] DiscardDeckView _discardDeckView = null;

    Deck<Card> _playerHand = new Deck<Card>();

    [SerializeField] CanvasGroup _playButtonCG = null;
    [SerializeField] CanvasGroup _passTurnCG = null;
    [SerializeField] CanvasGroup _drawButtonCG = null;
    [SerializeField] Text _handSizeText = null;

    [SerializeField] CardView _tweenCard = null;
    private GameObject _tweenCardGO;

    [Header("Values")]
    [SerializeField] int _currentCardIndex = 0;
    [SerializeField] int _shuffles = 3;
    [SerializeField] int _startHandSize = 3;
    [SerializeField] int _handLimit = 7;
    [HideInInspector] public bool _canDraw = false;

    private Card _cardToPlace = null;
    public Card CardToPlace => _cardToPlace;
    private GameObject _cardToPlaceGO = null;
    public GameObject CardToPlaceGO => _cardToPlaceGO;
    private CardView _cardToPlaceView = null;
    public CardView CardToPlaceView => _cardToPlaceView;

    private void Start()
    {
        SetupDrawDeck();
        _tweenCardGO = _tweenCard.gameObject;
    }

    private void SetupDrawDeck()
    {
        Debug.Log("Player: Creating ConvoCards...");
        foreach (ConvoCardData convoData in _convoDeck.ConvoDeckConfig)
        {
            ConvoCard newConvoCard = new ConvoCard(convoData);
            _drawDeck.Add(newConvoCard);
        }
        Debug.Log("Player: Creating EnviroCards...");
        foreach (EnviroCardData enviroData in _enviroDeck.EnviroDeckConfig)
        {
            EnviroCard newEnviroCard = new EnviroCard(enviroData);
            _drawDeck.Add(newEnviroCard);
        }

        Debug.Log("Player: Shuffling...");
        for(int i = 0; i < _shuffles; i++)
        {
            _drawDeck.Shuffle();
        }
        _drawDeckView.Display(_drawDeck);
        _discardDeckView.DisplayNull();
        _currentCardView.DisplayNull();
    }

    public void DrawStartingHand()
    {
        if (_drawDeck.Count < _startHandSize)
        {
            Debug.Log("Player: Shuffling discard into deck...");
            _drawDeck.Add(_discardDeck.Cards);
            _drawDeck.Shuffle();
            _discardDeck.Clear();
            _drawDeckView.Display(_drawDeck);
            _discardDeckView.DisplayNull();
        }
        for(int i = 0; i < _startHandSize; i++)
        {
            DrawCard();
        }
        CheckPassPlayState();
    }

    private void OnEnable()
    {
        _inputC.PressedLeft += OnPressedLeft;
        _inputC.PressedRight += OnPressedRight;
        _inputC.PressedDraw += OnPressedDraw;
        _inputC.PressedPlayCard += OnPressedPlay;
        _inputC.PressedDiscard += OnPressedDiscard;
    }

    private void OnDisable()
    {
        _inputC.PressedLeft -= OnPressedLeft;
        _inputC.PressedRight -= OnPressedRight;
        _inputC.PressedDraw -= OnPressedDraw;
        _inputC.PressedPlayCard -= OnPressedPlay;
        _inputC.PressedDiscard += OnPressedDiscard;
    }

    private void OnPressedPlay()
    {
        PlayCurrentCard();
    }

    private void OnPressedDraw()
    {
        if(_playerHand.Count < _handLimit)
        {
            if (_canDraw)
            {
                DrawCard();
            }
            else
            {
                Debug.LogWarning("Player: Already drew this turn!");
            }
        }
        else
        {
            Debug.LogWarning("Player: Hand size too large, please discard a card!");
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

    private void OnPressedDiscard()
    {
        if(_playerHand.Count > 0)
        {
            DiscardCurrentCard();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PrintPlayerHand();
        }
    }

    public void DrawCard()
    {
        Card newCard = _drawDeck.Draw(DeckPosition.Top);
        if (newCard != null)
        {
            Debug.Log("Player: Drew card: " + newCard.Name);
            _playerHand.Add(newCard, DeckPosition.Top);
            _currentCardIndex = _playerHand.Count - 1;
            StartCoroutine(DrawCardTween(newCard, 0.25f));
            _canDraw = false;
        }
        else
        {
            Debug.Log("Player: Shuffling discard into deck...");
            _drawDeck.Add(_discardDeck.Cards);
            StartCoroutine(ReshuffleTween(_discardDeck.TopItem, 0.25f));
            _drawDeck.Shuffle();
            _discardDeck.Clear();
            _canDraw = true;
        }
        CheckPassPlayState();
        UpdateHandSize();
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
            _cardToPlace = targetCard;
            //TODO Set TweenCard position
            _cardToPlaceGO = Instantiate(_tweenCardGO, _tweenCardGO.transform);
            _cardToPlaceView = _cardToPlaceGO.GetComponent<CardView>();
            _cardToPlaceGO.transform.position = _currentCardView.transform.position;
            _cardToPlaceView.Display(targetCard);
        }
        else
        {
            Debug.LogWarning("Player Hand: Nothing to play - hand is empty!");
        }
        UpdateHandSize();
    }

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
            Debug.LogWarning("Player: No cards to select between");
        }
        UpdateHandSize();
    }

    public void SelectPrevCard()
    {
        if (_playerHand.Count > 0)
        {
            if (_currentCardIndex - 1 >= 0)
            {
                //If there's a previous card to go to
                _currentCardIndex -= 1;
                _currentCardView.Display(_playerHand.Cards[_currentCardIndex]);
            }
            else
            {
                //else return to the end
                _currentCardIndex = _playerHand.Count - 1;
                _currentCardView.Display(_playerHand.TopItem);
            }
        }
        else
        {
            Debug.LogWarning("Player: No cards to select between");
        }
        UpdateHandSize();
    }

    public void CheckPassPlayState()
    {
        if (_canDraw)
        {
            _playButtonCG.alpha = 0;
            _passTurnCG.alpha = 0;
            _drawButtonCG.alpha = 1;
        }
        else
        {
            _playButtonCG.alpha = 1;
            _passTurnCG.alpha = 1;
            _drawButtonCG.alpha = 0;
        }
    }

    public void DiscardCurrentCard()
    {
        Card targetCard = _playerHand.Cards[_currentCardIndex];
        _playerHand.Remove(_currentCardIndex);
        _discardDeck.Add(targetCard);
        UpdateHandSize();
        UpdateHandVisuals();
        StartCoroutine(DiscardTween(targetCard, 0.25f));
    }

    public void DiscardPlayedCard()
    {
        Card targetCard = _playerHand.Cards[_currentCardIndex];
        _playerHand.Remove(_currentCardIndex);
        _discardDeck.Add(targetCard);
        UpdateHandSize();
        UpdateHandVisuals();
    }

    private void UpdateHandSize()
    {
        _handSizeText.text = _playerHand.Count.ToString() + "/" + _handLimit.ToString();
        if(_playerHand.Count >= _handLimit)
        {
            _handSizeText.color = Color.red;
        }
        else
        {
            _handSizeText.color = Color.black;
        }
    }

    public void UpdateHandVisuals()
    {
        if (_playerHand.Count > 0)
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

    private IEnumerator DrawCardTween(Card card, float duration)
    {
        GameObject tweenGO = Instantiate(_tweenCardGO, _tweenCardGO.transform);
        CardView tweenView = tweenGO.GetComponent<CardView>();
        tweenGO.transform.position = _drawDeckView.transform.position;
        tweenView.Display(card);
        LeanTween.move(tweenGO, _currentCardView.transform, duration);
        yield return new WaitForSeconds(duration);
        _currentCardView.Display(_playerHand.Cards[_currentCardIndex]);
        _drawDeckView.Display(_drawDeck);
        Destroy(tweenGO);
    }

    private IEnumerator ReshuffleTween(Card card, float duration)
    {
        GameObject tweenGO = Instantiate(_tweenCardGO, _tweenCardGO.transform);
        CardView tweenView = tweenGO.GetComponent<CardView>();
        tweenGO.transform.position = _discardDeckView.transform.position;
        tweenView.Display(card);
        LeanTween.move(tweenGO, _drawDeckView.transform, duration);
        yield return new WaitForSeconds(duration);
        _drawDeckView.Display(_drawDeck);
        _discardDeckView.DisplayNull();
        Destroy(tweenGO);
    }

    private IEnumerator DiscardTween(Card card, float duration)
    {
        GameObject tweenGO = Instantiate(_tweenCardGO, _tweenCardGO.transform);
        CardView tweenView = tweenGO.GetComponent<CardView>();
        tweenGO.transform.position = _currentCardView.transform.position;
        tweenView.Display(card);
        LeanTween.move(tweenGO, _discardDeckView.transform, duration);
        yield return new WaitForSeconds(duration);
        _discardDeckView.Display(card);
        Debug.Log("Player: Card added to discard: " + card.Name);
        Debug.Log("Player Hand Count: " + _playerHand.Count);
        if (_playerHand.Count > 0)
        {
            _currentCardView.Display(_playerHand.TopItem);
            _currentCardIndex = _playerHand.Count - 1;
        }
        else
        {
            _currentCardView.DisplayNull();
            _currentCardIndex = 0;
        }
        Destroy(tweenGO);
    }
}
