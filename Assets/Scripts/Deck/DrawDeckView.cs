using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawDeckView : MonoBehaviour
{
    [SerializeField] Text _cardCount = null;
    [SerializeField] Image _cardBackImage = null;
    [SerializeField] Sprite _cardBackSprite = null;

    public void Display(Deck<Card> draw)
    {
        _cardBackImage.sprite = _cardBackSprite;
        _cardCount.text = draw.Count.ToString();
    }
}
