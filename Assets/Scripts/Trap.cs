using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject bulletPrefab;
  
    void Start()
    {
        StartCoroutine(Spawn());
    }
    
    public IEnumerator Spawn()
    {
        while (true)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var direction = (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) *  0.03f);
            yield return new WaitForSeconds(1);

        }
    }
}
