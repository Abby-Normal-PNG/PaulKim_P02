using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] Text _nameText = null;
    [SerializeField] Text _costText = null;
    [SerializeField] Image _graphicUI = null;
    [SerializeField] Text _descriptionText = null;
    [SerializeField] Image _bgImage = null;
    [HideInInspector] public Color _bgColor = Color.green;

    public void Display(AbilityCard abilityCard)
    {
        _nameText.text = abilityCard.Name;
        _costText.text = abilityCard.Cost.ToString();
        _graphicUI.sprite = abilityCard.Graphic;
        _descriptionText.text = abilityCard.Description;
        _bgColor = abilityCard.Color;
        _bgImage.color = _bgColor;
    }
}
