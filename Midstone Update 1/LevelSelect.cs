using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button tutorialButton;
    public Button level1Button;
    public Button backButton;
    public string SceneName;

    public void Level1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
    }

    public void Tutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Menu");
    }
}
