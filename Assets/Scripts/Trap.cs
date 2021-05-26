using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject bullePrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (true)
        {


            var bullet = Instantiate(bullePrefab, transform.position, Quaternion.identity);


            var direction = (GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction.x, direction.y) *  0.02f);
            yield return new WaitForSeconds(2);

        }
    }

    // Update is called once per frame

}
