using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Sprite[] _musicIcons;

    [SerializeField] private Animator _menu;
    [SerializeField] private Animator _game;

    [SerializeField] private Image _musicImage;

    [SerializeField] private Texture2D texture;

    private void Start()
    {
        SetMusic();
    }

    public void FadeAnimation()
    {
        _menu.SetBool(Values.FadeTag, true);
    }
    public void Play()
    {
        _game.gameObject.SetActive(true);
        _game.SetTrigger(Values.AwakeTag);
        Values.CanPlay = true;
        _menu.gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MusicTrunOffOn()
    {
        Values.Music = !Values.Music;
        SetMusic();
    }

    private void SetMusic()
    {
        if (Values.Music)
        {
            _musicImage.sprite = _musicIcons[0];
            Values.MusicVolume = 1;
            AudioListener.volume = Values.MusicVolume;
        }
        else
        {
            _musicImage.sprite = _musicIcons[1];
            Values.MusicVolume = 0;
            AudioListener.volume = Values.MusicVolume;
        }
    }
    

}
