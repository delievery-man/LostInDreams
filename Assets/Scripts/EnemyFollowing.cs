using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyFollowing : MonoBehaviour
{
    private Transform player;
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
    
    void Update()
    {
        agent.SetDestination(target.position);
    }
}