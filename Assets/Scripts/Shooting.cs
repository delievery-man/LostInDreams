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
    public WeaponTypes weapon = WeaponTypes.Single;
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            var mousePos = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPos = (Vector2)transform.position;
            
            SoundManager.PlaySound("fire");
            
            if (weapon == WeaponTypes.Single)
            {
                var bullet = Instantiate(this.bullet, shotPoint.position, Quaternion.identity);
            
                var direction = (mousePos - playerPos).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletForce;
                bullet.GetComponent<bulletMovement>().damage = 2;
            }
            else if (weapon == WeaponTypes.ShotGun)
            {
                for (int i = -1; i < 2; i++)
                {
                    var bullet =
                        Instantiate(this.bullet, shotPoint.position, Quaternion.identity);
                    var mainDir = (mousePos - playerPos).normalized;
                    var direction = (i*Vector2.Perpendicular(mainDir)/5 + mainDir).normalized;
                    bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletForce;
                    bullet.GetComponent<bulletMovement>().damage = 1;
                }
            }
            
            
            // else if (weapon == "shotgun")
            // {
            //     for (int i = 0; i < 3; i++)
            //     {
            //         
            //     }
            // }
        

            // var bullet1 =
            // Instantiate(this.bullet, shotPoint.position, Quaternion.identity);
            // var bullet2 =
            //     Instantiate(this.bullet, shotPoint.position, Quaternion.identity);
            // var bullet3 =
            //     Instantiate(this.bullet, shotPoint.position, Quaternion.identity);
            

            // var direction1 =  (mousePos - playerPos).normalized;
            // var direction2 = (Vector2.Perpendicular(direction1)/6 + direction1).normalized;
            // var direction3 = (-Vector2.Perpendicular(direction1)/6 + direction1).normalized;
            // bullet1.GetComponent<Rigidbody2D>().velocity = direction1 * bulletForce;
            // bullet1.GetComponent<bulletMovement>().damage = 1;
            // bullet2.GetComponent<Rigidbody2D>().velocity = direction2 * bulletForce;
            // bullet2.GetComponent<bulletMovement>().damage = 1;
            // bullet3.GetComponent<Rigidbody2D>().velocity = direction3 * bulletForce;
            // bullet3.GetComponent<bulletMovement>().damage = 1;
            //
    

        }
    }

    public enum WeaponTypes
    {
        Single,
        ShotGun
        
    }
}
