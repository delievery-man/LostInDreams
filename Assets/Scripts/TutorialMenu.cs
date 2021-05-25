using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    public GameObject TutorialScreen;
    

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        var time = Time.time + 0.5f;
        yield return new WaitUntil(() => Time.time > time);
        Time.timeScale = 0f;
        TutorialScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseTutorial()
    {
        TutorialScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenTutorial()
    {
        TutorialScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
