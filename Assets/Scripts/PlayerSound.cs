using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private bool isRunning;
    private bool aPressed;
    private bool sPressed;
    private bool dPressed;
    private bool wPressed;

    // Start is called before the first frame update
    void Start()
    {
        aPressed = false;
        sPressed = false;
        dPressed = false;
        wPressed = false;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            aPressed = true;
        if (Input.GetKeyUp(KeyCode.A))
            aPressed = false;
        if (Input.GetKeyDown(KeyCode.S))
            sPressed = true;
        if (Input.GetKeyUp(KeyCode.S))
            sPressed = false;
        if (Input.GetKeyDown(KeyCode.D))
            dPressed = true;
        if (Input.GetKeyUp(KeyCode.D))
            dPressed = false;
        if (Input.GetKeyDown(KeyCode.W))
            wPressed = true;
        if (Input.GetKeyUp(KeyCode.W))
            wPressed = false;
        
        PlaySound();
    }

    private void PlaySound()
    {
        if ((aPressed || sPressed || dPressed || wPressed) && !isRunning)
        {
            SoundManager.PlaySound("run");
            isRunning = true;
        }
        if (!aPressed && !sPressed && !dPressed && !wPressed && isRunning)
        {
            SoundManager.StopSound();
            isRunning = false;
        }
    }
}
