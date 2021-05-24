using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawFromInventory : MonoBehaviour
{
    public GameObject item;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem(int i)
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y - 1);
        Instantiate(item, playerPos, Quaternion.identity);
        player.GetComponent<Inventory>().isTaken[i] = false;
    }
}
