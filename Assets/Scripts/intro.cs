using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    public GameObject speechCloud;

    public GameObject text;

    public AudioSource tvSound;

    public GameObject playerTv;

    public GameObject playerSleep;

    void Start()
    {
        StartCoroutine(WaitForTV());
        StartCoroutine(WaitForText());
        StartCoroutine(Wait());
        StartCoroutine(WaitForSleep());

    }

    private IEnumerator WaitForTV()
    {
        var time = Time.time + 5f;
        yield return new WaitUntil(() => Time.time > time);
        speechCloud.SetActive(true);
        text.SetActive(true);
        tvSound.volume = 0;
    }
    
    private IEnumerator WaitForText()
    {
        var time = Time.time + 7.5f;
        yield return new WaitUntil(() => Time.time > time);
        text.SetActive(false);
        speechCloud.SetActive(false);
        playerTv.SetActive(false);
    }

    private IEnumerator Wait()
    {
        var time = Time.time + 9f;
        yield return new WaitUntil(() => Time.time > time);
        playerSleep.SetActive(true);
    }

    private static IEnumerator WaitForSleep()
    {
        var time = Time.time + 13f;
        yield return new WaitUntil(() => Time.time > time);
        SceneManager.LoadScene("SampleScene");
    }
}
