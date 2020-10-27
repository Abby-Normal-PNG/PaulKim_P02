using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscardDeckView : MonoBehaviour
{
    [SerializeField] Text _nameText = null;
    [SerializeField] Image _graphicUI = null;
    [SerializeField] Text _descriptionText = null;
    [SerializeField] Image _bgImage = null;
    [HideInInspector] public Color _bgColor = Color.green;
    [SerializeField] CanvasGroup _deckCG = null;

    public void Display(Card convoCard)
    {
        _deckCG.alpha = 1;
        _nameText.text = convoCard.Name;
        _graphicUI.sprite = convoCard.Graphic;
        _descriptionText.text = convoCard.Description;
        _bgColor = convoCard.Color;
        _bgImage.color = _bgColor;
    }

    public void DisplayNull()
    {
        _deckCG.alpha = 0;
    }
}
