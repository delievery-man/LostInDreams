using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health;

    public float maxHealth;
    private UnityEngine.Object explosion;
    public float vision;
    public Vector3 spawnRoom;
    public float damage = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        explosion = Resources.Load("Explosion");
        spawnRoom = GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().currRoom;
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
            var currRoom = GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().currRoom;
            GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().enemyCounters[spawnRoom][0]--;
            Destroy(gameObject);
            Destroy(explosionRef, 3);
        };
    }
    
    private void CheckOverHeal()
    {
        if (health >= maxHealth)
        {
            health = maxHealth;
        };
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<DealDamage>().PlayerDealDamage(damage);
            print( other.gameObject.GetComponent<DealDamage>().health);

        }
    }
}
