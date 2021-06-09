using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float damage;
    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")  || other.CompareTag("Wall"))
            {
                if (other.GetComponent<Enemy>() != null && !triggered)
                {
                    triggered = true;
                    other.GetComponent<Enemy>().DealDamage(damage);
                }
                else if (other.GetComponent<Boss>() != null)
                {
                    other.GetComponent<Boss>().DealDamage(damage);
                }
                Destroy(gameObject);

            }
    }
}
