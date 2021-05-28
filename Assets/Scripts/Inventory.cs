using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public List<GameObject> slots;
    public List<bool> isTaken;
    public Item[] itemsList;

    void Start()
    {
        itemsList = new Item[slots.Count];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            DropItem(slots[0], 0);
        else if (Input.GetKeyDown(KeyCode.X))
            DropItem(slots[1], 1);
        else if (Input.GetKeyDown(KeyCode.C))
            DropItem(slots[2], 2);
        else if (Input.GetKeyDown(KeyCode.V))
            DropItem(slots[3], 3);
    
    }

    private void DropItem(GameObject slot, int i)
    {
        foreach (Transform child in slot.transform)
        {
            if (itemsList[i].itemType == Item.ItemType.Key)
            {
                child.GetComponent<SpawnFromInventory>().SpawnDroppedItem(i);
            }
            else if (itemsList[i].itemType == Item.ItemType.Salve)
            {
                GetComponent<DealDamage>().health++;
                
            }
            isTaken[i] = false;

            Destroy(child.gameObject);
        }
    }
}

public class Item
{
    public enum ItemType
    {
        Key,
        Salve
    }

    public ItemType itemType;
}