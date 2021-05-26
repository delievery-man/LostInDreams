using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health;

    public float maxHealth;
    private Object explosion;
    public float vision;
    public Vector3 spawnRoom;
    public float damage = 2;
    public List<Transform> Loot;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        explosion = Resources.Load("Explosion");
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
            Destroy(gameObject);
            Destroy(explosionRef, 3);
            var deathPoint = gameObject.GetComponent<Transform>().position;
            Instantiate(Loot[0], deathPoint, Quaternion.identity);
            
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
