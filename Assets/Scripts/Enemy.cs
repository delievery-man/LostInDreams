using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    public float maxHealth;
    private UnityEngine.Object explosion;
    public float vision;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        explosion = Resources.Load("Explosion");
    }


    public void DealDamage(float damage)
    {
        health -= damage;
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = transform.position;
            Destroy(gameObject);
        };
    }
    
    private void CheckOverHeal()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        };
    }
}
