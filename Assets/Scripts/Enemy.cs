using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health;

    public float maxHealth;
    private UnityEngine.Object explosion;
    public float vision;
    public Vector3Int spawnRoom;
    public float damage = 2;
    public List<Transform> Loot;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        explosion = Resources.Load("Explosion");
        spawnRoom = GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().currRoom;
    }


    public void DealDamage(float damage)
    {
        if (health>0)
        {
            health -= damage;
            CheckDeath();
        }

    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = transform.position;
            var currRoom = GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().currRoom;
            GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().enemyCounters[spawnRoom][0]--;
            Debug.Log(GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGennerator>().enemyCounters[spawnRoom][0]);
            Destroy(gameObject);
            Destroy(explosionRef, 3);
            var deathPoint = gameObject.GetComponent<Transform>().position;
            if (Random.Range(1, 3) == 1)
            {
                Instantiate(Loot[Random.Range(0, Loot.Count)], deathPoint, Quaternion.identity);
            }
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


        }
    }
}
