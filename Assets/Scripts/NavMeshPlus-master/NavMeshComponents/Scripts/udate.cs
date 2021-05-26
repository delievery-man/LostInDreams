using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class udate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshSurface2d>().BuildNavMesh();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
