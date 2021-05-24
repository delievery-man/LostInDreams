using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Player" && other.tag !="Bullet" && other.tag != "Key" && other.tag != "Salve"  && other.tag != "Exit")
            {
                if (other.GetComponent<Enemy>() != null && !triggered)
                {
                    triggered = true;
                    other.GetComponent<Enemy>().DealDamage(damage);
                }
                Destroy(gameObject);

            }
    }
}
