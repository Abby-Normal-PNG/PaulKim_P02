using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] AudioClip _mainMenuBGM;
    [SerializeField] AudioClip _gameplayBGM;
    [SerializeField] AudioClip _waitingBGM;
    [SerializeField] AudioClip _victoryBGM;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayMenuBGM()
    {
        _source.Stop();
        _source.clip = _mainMenuBGM;
        _source.Play();
    }

    public void PlayGameplayBGM()
    {
        _source.Stop();
        _source.clip = _gameplayBGM;
        _source.Play();
    }

    public void PlayWaitingBGM()
    {
        _source.Stop();
        _source.clip = _waitingBGM;
        _source.Play();
    }

    public void PlayVictoryBGM()
    {
        _source.Stop();
        _source.clip = _victoryBGM;
        _source.Play();
    }

    public void SilenceBGM()
    {
        _source.Stop();
    }
}
