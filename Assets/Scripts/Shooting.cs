using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("Bullet")] public GameObject bullet;
    public float minDamage;
    public float maxDamage;
    public float bulletForce;
    public Transform shotPoint;
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bullet =
            Instantiate(this.bullet, shotPoint.position, Quaternion.identity);
            var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPos = (Vector2)transform.position; 
            var direction = (Vector2) (mousePos - playerPos).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletForce;
            bullet.GetComponent<bulletMovement>().damage = 1;

        }
    }
}
