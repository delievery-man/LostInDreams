using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class intro : MonoBehaviour
{
    public GameObject speechCloud;
    public GameObject words;
    public AudioSource tvSound;
    public GameObject playerTv;
    public GameObject playerSleep;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;

    [SerializeField] private TextWriter textWriter;
    private int sceneNumber = 0;
    private bool sceneFinished = false;
    private bool tvFinished = false;
    private bool gameStarted = false;

    void Start()
    {
        Scene0();
        sceneNumber++;
        // StartCoroutine(Scene1());
        // StartCoroutine(Scene2());
        // StartCoroutine(StartGameAuto());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (sceneNumber == 1)
            {
                if (!sceneFinished)
                {
                    FinishScene0();
                    sceneFinished = true;
                }
                else
                {
                    Scene1();
                    sceneFinished = false;
                    sceneNumber++;
                }
            }
            else if (sceneNumber == 2)
            {
                if (!sceneFinished)
                {
                    FinishScene1();
                    sceneFinished = true;
                }
                else
                {
                    Scene2();
                    sceneFinished = false;
                    sceneNumber++;
                }
            }
            else
            {
                if (!sceneFinished)
                {
                    FinishScene2();
                    sceneFinished = true;
                }
                else
                {
                    gameStarted = true;
                    StartGame();
                    sceneNumber++;
                }
            }
        }
    }

    private void Scene0()
    {
        var textMessage = transform.Find("Text 1").GetComponent<Text>();
        textWriter.AddWriter(textMessage, 
            "Родители нам часто говорят: \"Насмотришься ужастиков на ночь, и будут сниться кошмары\"", 
            0.05f);
        StartCoroutine(WaitForTV());
    }
    
    private void FinishScene0()
    {
        tvFinished = true;
        textWriter.StopWriter();
        var textMessage = transform.Find("Text 1").GetComponent<Text>();
        textMessage.text = "Родители нам часто говорят: \"Насмотришься ужастиков на ночь, и будут сниться кошмары\"";
    }

    private IEnumerator WaitForTV()
    {
        var time = Time.time + 5f;
        yield return new WaitUntil(() => Time.time > time);
        if (!tvFinished)
        {
            speechCloud.SetActive(true);
            words.SetActive(true);
        }
        tvSound.volume = 0;
    }
    
    private IEnumerator WaitForText()
    {
        var time = Time.time + 7.5f;
        yield return new WaitUntil(() => Time.time > time);
        words.SetActive(false);
        speechCloud.SetActive(false);
        playerTv.SetActive(false);
        text1.SetActive(false);
    }


    private void Scene1()
    {
        // var time = Time.time + 9f;
        // yield return new WaitUntil(() => Time.time > time);
        words.SetActive(false);
        speechCloud.SetActive(false);
        playerTv.SetActive(false);
        text1.SetActive(false);
        playerSleep.SetActive(true);
        text2.SetActive(true);
        var textMessage = transform.Find("Text 2").GetComponent<Text>();
        textWriter.AddWriter(textMessage, "Мы им не верим", 0.05f);
        
    }

    private void FinishScene1()
    {
        textWriter.StopWriter();
        var textMessage = transform.Find("Text 2").GetComponent<Text>();
        textMessage.text = "Мы им не верим";
    }
    
    private void Scene2()
    {
        // var time = Time.time + 13f;
        // yield return new WaitUntil(() => Time.time > time);
        playerSleep.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(true);
        var textMessage = transform.Find("Text 3").GetComponent<Text>();
        textWriter.AddWriter(textMessage, "А зря...", 0.3f);
        StartCoroutine(StartGameAuto());    
    }
    
    private void FinishScene2()
    {
        textWriter.StopWriter();
        var textMessage = transform.Find("Text 3").GetComponent<Text>();
        textMessage.text = "А зря...";
    }

    private IEnumerator StartGameAuto()
    {
        var time = Time.time + 4f;
        yield return new WaitUntil(() => Time.time > time);
        if (!gameStarted)
            SceneManager.LoadScene("SimpleLevel");
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SimpleLevel");
    }
}
