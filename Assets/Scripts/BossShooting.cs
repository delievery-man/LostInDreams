using System.Collections;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
  
    void Start()
    {
        StartCoroutine(ShootAround());
    }

    private IEnumerator ShootAround()
    {
        while (true)
        {
            foreach (var dir in Direction.directionsDiag)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                var direction = dir.normalized;
                bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) *  0.03f);
            }
            
            yield return new WaitForSeconds(1);
        }
    }
}
