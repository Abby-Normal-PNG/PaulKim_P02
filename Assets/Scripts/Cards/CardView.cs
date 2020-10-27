using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardView : MonoBehaviour
{
    [SerializeField] Text _nameText = null;
    [SerializeField] Image _graphicUI = null;
    [SerializeField] Text _descriptionText = null;
    [SerializeField] Image _bgImage = null;
    [HideInInspector] public Color _bgColor = Color.green;
    [SerializeField] CanvasGroup _cardCG = null;
    
    public void Display(Card card)
    {
        _cardCG.alpha = 1;
        _nameText.text = card.Name;
        //Debug.Log("Card Name: " + card.Name);
        _graphicUI.sprite = card.Graphic;
        //Debug.Log("Card Graphic: " + card.Graphic.name);
        _descriptionText.text = card.Description;
        //Debug.Log("Card Description: " + card.Description);
        _bgColor = card.Color;
        //Debug.Log("Card Color: " + card.Color);
        _bgImage.color = _bgColor;
    }

    public void Display(ConvoCard convoCard)
    {
        _cardCG.alpha = 1;
        _nameText.text = convoCard.Name;
        _graphicUI.sprite = convoCard.Graphic;
        _descriptionText.text = convoCard.Description;
        _bgColor = convoCard.Color;
        _bgImage.color = _bgColor;
    }

    public void Display(AbilityCard abilityCard)
    {
        _nameText.text = abilityCard.Name;
        //_costText.text = abilityCard.Cost.ToString();
        _graphicUI.sprite = abilityCard.Graphic;
        _descriptionText.text = abilityCard.Description;
        _bgColor = abilityCard.Color;
        _bgImage.color = _bgColor;
    }

    public void DisplayNull()
    {
        _cardCG.alpha = 0;
    }
}
