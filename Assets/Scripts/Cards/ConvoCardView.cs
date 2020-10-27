using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoCardView : MonoBehaviour
{
    [SerializeField] Text _nameText = null;
    [SerializeField] Image _graphicUI = null;
    [SerializeField] Text _descriptionText = null;
    [SerializeField] Image _bgImage = null;
    [HideInInspector] public Color _bgColor = Color.green;

    public void Display(ConvoCard convoCard)
    {
        _nameText.text = convoCard.Name;
        _graphicUI.sprite = convoCard.Graphic;
        _descriptionText.text = convoCard.Description;
        _bgColor = convoCard.Color;
        _bgImage.color = _bgColor;
    }
}
