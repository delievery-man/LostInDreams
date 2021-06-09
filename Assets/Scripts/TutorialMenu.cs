using System;
using System.Collections;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject TutorialScreen;

    public void CloseTutorial()
    {
        TutorialScreen.SetActive(false);
        Time.timeScale = 1f;
        Shooting.StartShooting();
    }

    public void OpenTutorial()
    {
        TutorialScreen.SetActive(true);
        Time.timeScale = 0f;
        Shooting.StopShooting();
    }
}
