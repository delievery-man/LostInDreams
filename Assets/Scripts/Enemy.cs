using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Object explosion;
    public Vector3Int spawnRoom;
    public float damage = 2;
    public List<Transform> Loot;
    public bool isTested;
    
    void Start()
    {
        health = maxHealth;
        explosion = Resources.Load("Explosion");
        if (!isTested)
        {
            spawnRoom = GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGenerator>().currRoom;

        }
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
            GameObject explosionRef = (GameObject) Instantiate(explosion);
            explosionRef.transform.position = transform.position;
            if (!isTested)
            {
                GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGenerator>()
                    .enemyCounters[spawnRoom][0]--;
                Debug.Log(GameObject.FindGameObjectWithTag("Generator").GetComponent<BSPGenerator>()
                    .enemyCounters[spawnRoom][0]);

                SoundManager.PlaySound("enemy");

                var deathPoint = gameObject.GetComponent<Transform>().position;
                if (Random.Range(1, 4) == 1)
                {
                    Instantiate(Loot[Random.Range(0, Loot.Count)], deathPoint, Quaternion.identity);
                }
            }
            
            Destroy(gameObject);
            Destroy(explosionRef, 3);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DealDamage>().PlayerDealDamage(damage);
        }
    }
}
