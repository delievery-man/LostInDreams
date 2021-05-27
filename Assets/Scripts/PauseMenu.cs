using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool PauseGame;
    public GameObject PauseGameMenu;
    public AudioMixerSnapshot Paused;
    public AudioMixerSnapshot Unpaused;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;
        Unpaused.TransitionTo(.01f);
        Shooting.StartShooting();
    }

    public void Pause()
    {
        PauseGameMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;
        Paused.TransitionTo(.01f);
        Shooting.StopShooting();

    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Shooting.StartShooting();
        Unpaused.TransitionTo(.01f);
        var curScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(curScene);
    }
    
}
