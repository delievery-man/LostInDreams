using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Time.timeScale = 1f;
        // SceneManager.LoadScene("SampleScene");
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
