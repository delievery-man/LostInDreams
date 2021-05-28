using UnityEngine;

public class SpawnFromInventory : MonoBehaviour
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
