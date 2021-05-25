using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PickUp : MonoBehaviour
{
    public GameObject item;
    public GameObject itemImage;
    private Inventory inventory;
    
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        for (var i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.isTaken[i]) continue;
            inventory.isTaken[i] = true;
            if (item.gameObject.CompareTag("Key"))
            {
                inventory.itemsList[i] = new Item {itemType = Item.ItemType.Key};
                Instantiate(itemImage, inventory.slots[i].transform.position, Quaternion.identity,
                    inventory.slots[i].transform);
            }

            if (item.gameObject.CompareTag("Salve"))
            {
                inventory.itemsList[i] = new Item {itemType = Item.ItemType.Salve};
                Instantiate(itemImage, inventory.slots[i].transform.position, Quaternion.identity,
                    inventory.slots[i].transform);
            }

            if (item.gameObject.CompareTag("ShotGun"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>().weapon =
                    Shooting.WeaponTypes.ShotGun;
                inventory.isTaken[i] = false;
            }
            if (item.gameObject.CompareTag("Shield"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<DealDamage>().isInvulnerable = true;
                inventory.isTaken[i] = false;
                ShieldTimer(item);

                
            }

            Destroy(item);
            break;
        }
    }

    private void ShieldTimer(GameObject item)
    {
        Destroy(item);
        var duration = 5f;
        while (duration>0)
        {
            duration -= Time.deltaTime;
        }


    }

   
}