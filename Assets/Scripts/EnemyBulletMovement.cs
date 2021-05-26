using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Bullet" || other.tag == "Wall")
            {
                if (other.GetComponent<DealDamage>() != null)
                {
                    other.GetComponent<DealDamage>().PlayerDealDamage(damage);
                }
                Destroy(gameObject);

            }
    }
}
