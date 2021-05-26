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
    void Start()
    {
        var textMessage = transform.Find("Text 1").GetComponent<Text>();
        textWriter.AddWriter(textMessage, "Родители нам часто говорят: \"Насмотришься ужастиков на ночь, и будут сниться кошмары\"", 0.05f);
        

        StartCoroutine(WaitForTV());
        StartCoroutine(WaitForText());
        StartCoroutine(Wait());
        StartCoroutine(WaitForSleep());
        StartCoroutine(WaitForEnd());

    }

    private IEnumerator WaitForTV()
    {
        var time = Time.time + 5f;
        yield return new WaitUntil(() => Time.time > time);
        speechCloud.SetActive(true);
        words.SetActive(true);
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

    private IEnumerator Wait()
    {
        var time = Time.time + 9f;
        yield return new WaitUntil(() => Time.time > time);
        playerSleep.SetActive(true);
        text2.SetActive(true);
        var textMessage = transform.Find("Text 2").GetComponent<Text>();
        textWriter.AddWriter(textMessage, "Мы им не верим", 0.05f);
    }


    private IEnumerator WaitForSleep()
    {
        var time = Time.time + 13f;
        yield return new WaitUntil(() => Time.time > time);
        playerSleep.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(true);
        var textMessage = transform.Find("Text 3").GetComponent<Text>();
        textWriter.AddWriter(textMessage, "А зря...", 0.3f);
    }

    private static IEnumerator WaitForEnd()
    {
        var time = Time.time + 17f;
        yield return new WaitUntil(() => Time.time > time);
        
        SceneManager.LoadScene("SimpleLevel");
        
    }
}
