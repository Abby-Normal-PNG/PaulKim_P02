using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCard : MonoBehaviour
{
    [SerializeField] MeshRenderer _cardArtMesh = null;
    private Texture2D _cardArtTexture = null;

    public void SetTexture(Card card)
    {
        _cardArtTexture = card.CardTexture;
        _cardArtMesh.material.SetTexture("_MainTex", _cardArtTexture);
    }
}
