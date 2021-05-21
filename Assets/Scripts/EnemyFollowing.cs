using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;

public class EnemyFollowing : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Transform player;
    public float vision;
    public Tilemap floor;
    public Vector3 roomCenter;
    private float direction = 1f; // направление, 1 - вправо, -1 влево
    private float powerForce = 5f;
    private NavMeshAgent agent;
    public Transform target;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        
    }

 
}