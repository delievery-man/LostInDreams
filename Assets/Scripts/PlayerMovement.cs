using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    private Vector2 direction;

    private Animator _animator;
    public bool isTested;
    public Inventory inventory;
    public GameObject keyImage;
    public GameObject salveImage;
    public GameObject shield;
    public Shield shieldTimer;
    public shotTimer shotTimer;

    void Start()
    {
        _animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
    }
    
    void Update()
    {
        TakeInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.MovePosition(rb.position + direction * (speed * Time.fixedDeltaTime));
        if (direction.x != 0 || direction.y != 0)
            SetAnimator(direction);
        
        else
            _animator.SetFloat("speed", 0);
    }

    private void TakeInput()
    {
        direction.x = (Input.GetAxisRaw("Horizontal"));
        direction.y = (Input.GetAxisRaw("Vertical"));
    }

    private void SetAnimator(Vector2 direction)
    {
        _animator.SetFloat("xDir", direction.x);
        _animator.SetFloat("yDir", direction.y);
        _animator.SetFloat("speed", direction.sqrMagnitude);
    }

    private bool IsInventoryFool()
    {
        var count = inventory.isTaken.Count(t => t);

        return count == inventory.isTaken.Count;
    }
    
    
    public void OnTriggerEnter2D(Collider2D other)
    {   
        if (!IsInventoryFool())
        {
            if (other.gameObject.CompareTag("Key"))
            {
                for (var i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.isTaken[i])
                        continue;
                    inventory.isTaken[i] = true;
                    inventory.itemsList[i] = new Item {itemType = Item.ItemType.Key};
                    Instantiate(keyImage, inventory.slots[i].transform.position, Quaternion.identity,
                        inventory.slots[i].transform);
                    break;
                }

                Destroy(other.gameObject);
                SoundManager.PlaySound("key");
            }



            else if (other.gameObject.CompareTag("Salve"))
            {
                for (var i = 0; i < inventory.slots.Count; i++)
                {
                    if (inventory.isTaken[i]) continue;
                    inventory.isTaken[i] = true;
                    inventory.itemsList[i] = new Item {itemType = Item.ItemType.Salve};
                    if (!isTested)
                    {
                        Instantiate(salveImage, inventory.slots[i].transform.position, Quaternion.identity,
                            inventory.slots[i].transform);
                    }
                       
                    break;
                }

                Destroy(other.gameObject);
            }
        }
            
        if (other.gameObject.CompareTag("ShotGun"))
        {
            GetComponent<Shooting>().weapon =
                Shooting.WeaponTypes.ShotGun;
            shotTimer.ResetTimer();
            shotTimer.gameObject.SetActive(true);
            shotTimer.isCd = true;
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Shield"))
        { 
            
            shield.SetActive(true);
            shieldTimer.gameObject.SetActive(true);
            shieldTimer.isCd = true;
            Destroy(other.gameObject);
           
        }
        
    }
}
