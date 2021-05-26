using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("HardLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
