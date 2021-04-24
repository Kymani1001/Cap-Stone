using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;
    public string SceneName;
    
    
    public void StartGame()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneName);
        SceneManager.LoadScene("Level Select");
    }


    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
