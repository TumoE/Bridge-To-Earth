using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject _earth;
    [SerializeField] private GameObject _gameUi;
    [SerializeField] private GameObject _bgMusic;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _nightMusic;
    [SerializeField] private PlayerAudioManager _playerMusic;

    public void ShowEarth()
    {
        _playerMusic.Stop();
        _playerMusic.SetActiveWalkSound(false);
        _bgMusic.SetActive(false);
        _gameUi.SetActive(false);
        _earth.SetActive(true);
        _nightMusic.SetActive(false);
    }

    public void ShowWinScreen()
    {
        _winScreen.SetActive(true);
    } 
}
