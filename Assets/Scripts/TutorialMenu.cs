using System;
using System.Collections;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject TutorialScreen;
    private bool isOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isOpen)
            {
                CloseTutorial();
            }
            else
            {
                OpenTutorial();
            }
        }
    }

    public void CloseTutorial()
    {
        TutorialScreen.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
    }

    public void OpenTutorial()
    {
        TutorialScreen.SetActive(true);
        Time.timeScale = 0f;
        isOpen = true;
    }
}
