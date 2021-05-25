using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DealDamage : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    
    public int numOfHearts;
    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool isInvulnerable = false;
    public Shield shieldTimer;
    

    private void FixedUpdate()
    {
        if (health>numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            if (i<Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public void PlayerDealDamage(float damage)
    {
        if (!shieldTimer.GetComponent<Shield>().isCd && health>0)
        {
            health -= damage;
            CheckDeath();
        }
        
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
    }
}
