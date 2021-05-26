using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    private Text uiText;

    private string textToWrite;

    private int charIndex;

    private float timePerChar;

    private float timer;

    public void AddWriter(Text uiText, string textToWrite, float timePerChar)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerChar = timePerChar;
        charIndex = 0;
    }
    
    void Update()
    {
        if (!(uiText is null))
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerChar;
                charIndex++;
                uiText.text = textToWrite.Substring(0, charIndex);
                if (charIndex >= textToWrite.Length)
                {
                    uiText = null;
                }
            }
        }
    }
}
