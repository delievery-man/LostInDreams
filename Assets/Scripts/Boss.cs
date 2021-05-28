using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health;

    public float maxHealth;
    private Object explosion;
    public float damage = 2;
    public List<Transform> loot;
    
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
            SoundManager.PlaySound("boss");
            Destroy(explosionRef, 3);
            var deathPoint = gameObject.GetComponent<Transform>().position;
            Instantiate(loot[0], deathPoint, Quaternion.identity);
            
        };
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DealDamage>().PlayerDealDamage(damage);
        }
    }
}
