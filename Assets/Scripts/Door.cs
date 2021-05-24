using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Animator animator;

    public bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        var time = Time.time + 1.7f;
        yield return new WaitUntil(() => Time.time > time);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}