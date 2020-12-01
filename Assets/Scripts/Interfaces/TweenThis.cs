using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenThis : MonoBehaviour
{
    [SerializeField] LeanTweenType _easeType;
    [SerializeField] float _endSize = 1f;
    [SerializeField] float _animTime = 1f;
    private CanvasGroup _cg;

    private void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void PopIn()
    {
        if(_cg != null)
        {
            _cg.alpha = 0;
            _cg.interactable = true;
            _cg.blocksRaycasts = true;
            _cg.LeanAlpha(1, _animTime);
        }
        Vector3 scaleVector = Vector3.one * _endSize;
        gameObject.transform.localScale = Vector3.one * 0.1f;
        LeanTween.scale(this.gameObject, scaleVector, _animTime).setEase(_easeType);
    }

    public void PopOut()
    {
        if (_cg != null)
        {
            _cg.alpha = 1;
            _cg.interactable = false;
            _cg.blocksRaycasts = false;
            _cg.LeanAlpha(0, _animTime);
        }
        Vector3 scaleVector = Vector3.one * _endSize;
        gameObject.transform.localScale = scaleVector;
        LeanTween.scale(this.gameObject, Vector3.zero, _animTime).setEase(_easeType);
    }

    public IEnumerator BeatBump()
    {
        Vector3 scaleVector = Vector3.one * _endSize;
        gameObject.transform.localScale = scaleVector;
        LeanTween.scale(this.gameObject, scaleVector/2, _animTime/2).setEase(_easeType);
        yield return new WaitForSeconds(_animTime / 2);
        LeanTween.scale(this.gameObject, scaleVector, _animTime/2).setEase(_easeType);
    }

    public void ClickBeat()
    {
        StartCoroutine(BeatBump());
    }
}
