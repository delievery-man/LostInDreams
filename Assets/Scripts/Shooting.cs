using UnityEngine;
using UnityEngine.Serialization;

public class Shooting : MonoBehaviour
{
    [FormerlySerializedAs("Bullet")] public GameObject bullet;
    public float bulletForce;
    public Transform shotPoint;
    public WeaponTypes weapon = WeaponTypes.Single;
    private static bool isPaused;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPaused)
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
        }
    }

    public static void StopShooting()
    {
        isPaused = true;
    }
    public static void StartShooting()
    {
        isPaused = false;
    }

    public enum WeaponTypes
    {
        Single,
        ShotGun
    }
}
