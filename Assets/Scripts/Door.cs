using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            StartCoroutine(Waiter());
        }
    }

    private IEnumerator Waiter()
    {
        animator.SetBool("Open", true);
        SoundManager.PlaySound("door");
        var time = Time.time + 1.7f;
        yield return new WaitUntil(() => Time.time > time);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}