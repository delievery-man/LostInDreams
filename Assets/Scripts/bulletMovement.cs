using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Player")
            {
                if (other.GetComponent<Enemy>() != null)
                {
                    other.GetComponent<Enemy>().DealDamage(damage);
                }
                Destroy(gameObject);
            }
    }
}
