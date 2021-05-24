using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.tag !="Bullet" && other.tag != "Key" && other.tag != "Salve"  && other.tag != "Exit" && other.tag != "Enemy")
            {
                if (other.GetComponent<DealDamage>() != null)
                {
                    other.GetComponent<DealDamage>().PlayerDealDamage(damage);
                }
                Destroy(gameObject);

            }
    }
}
