using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet") || other.CompareTag("Wall"))
        {
            if (other.GetComponent<DealDamage>() != null)
            {
                other.GetComponent<DealDamage>().PlayerDealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
