using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public GameObject bullePrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootForward());
    }

    private IEnumerator ShootForward()
    {
        while (true)
        {

            var bullet = Instantiate(bullePrefab, transform.position, Quaternion.identity);
            var direction = Vector2.up;
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) *  0.03f);
            
            
            yield return new WaitForSeconds(1);
        }
    }
}
