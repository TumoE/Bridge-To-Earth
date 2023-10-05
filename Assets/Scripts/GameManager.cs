using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Script;

    [SerializeField] private GameObject _loseScreen;

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _pause;

    private bool isPaused;

    void Awake()
    {
        Script = this;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }

    public void Lose()
    {
        _loseScreen.SetActive(true);
        Values.CanPlay = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {

    }
    public void SetPause()
    {
        if (_menu.activeInHierarchy) return;

        isPaused = !isPaused;
        _pause.SetActive(isPaused);
        Values.CanPlay = !isPaused;
        AudioListener.pause = isPaused;
    }
}
