using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DealDamage : MonoBehaviour
{
    public float health;
    public int numOfHearts;
    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool isInvulnerable;
    public bool isDead;
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

        isInvulnerable = shieldTimer.GetComponent<Shield>().isCd;
    }
    
    public void PlayerDealDamage(float damage)
    {
        if (!isInvulnerable && health >0)
        {
            health -= damage;
            CheckDeath();
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            SoundManager.PlaySound("player");
            isDead = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
    }
}
