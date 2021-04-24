using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseScreen, ResumeButton, RetryButton, ReturnButton, PauseButton, LevelButton;
    public string SceneName;
    
    
    public void Paused()
    {
        PauseScreen.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    
    public void Resume()
    {
        PauseScreen.SetActive(false);
        PauseButton.SetActive(true);
       Time.timeScale = 1;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }

    public void Return()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void Level()
    {
        SceneManager.LoadScene("Level Select");
    }
}
