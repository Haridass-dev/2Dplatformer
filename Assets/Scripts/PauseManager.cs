using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    public GameObject _pauseScreen;

    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void PausePlay(bool _play)
    {
        _pauseScreen.SetActive(!_play);
        Time.timeScale = _play ? 1f : 0f;
        AudioListener.pause = !_play;
    }


    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
