using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private PlayerMovement player;
    public Animator animator;

    public bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (isFinished)
        // {
        //      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.isPicked)
            {
                StartCoroutine(waiter());
            }
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
