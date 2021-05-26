using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bedroom : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        var time = Time.time + 4.5f;
        yield return new WaitUntil(() => Time.time > time);
        SceneManager.LoadScene("SampleScene");
    }
}
