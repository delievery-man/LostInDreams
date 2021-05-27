using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator animator;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            StartCoroutine(waiter());
        }
    }
    
    IEnumerator waiter()
    {
        animator.SetBool("Open", true);
        SoundManager.PlaySound("door");
        var time = Time.time + 1.7f;
        yield return new WaitUntil(() => Time.time > time);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}